using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Characters;
using ConsoleApp8.Interface;
using ConsoleApp8.Monsters;
using ConsoleApp8.Scripts.Characters;

namespace ConsoleApp8.Scenes
{
    public enum BattleState
    {
        PlayerTurn_SelectAction, // 플레이어 행동 선택
        PlayerTurn_SelectTarget, // 플레이어 공격 대상 선택
        PlayerTurn_SelectSkill, //  스킬 선택
        PlayerTurn_SelectSkillTarget, //  스킬 대상 선택

        PlayerTurn_ShowResult,   // 플레이어 공격 결과 표시
        EnemyTurn_ShowResult,    // 몬스터 공격 결과 표시
        BattleOver_Victory,      // 전투 승리
        BattleOver_Lose          // 전투 패배

    }

    public class BattleScene : IScene
    {
        private readonly Character _player;
        private readonly IScene _previousScene; // 돌아갈 이전 Scene (MainScene)
        private readonly List<Monster> _monsters;
        private readonly Random _random;
        private BattleState _currentState;
        private Monster _lastAttackedMonster = null; // 플레이어가 마지막으로 공격한 몬스터
        private int _lastDamageDealt = 0; // 플레이어가 마지막으로 입힌 데미지
        private Monster _currentAttackingMonster = null; // 현재 공격 중인 몬스터
        private int _enemyAttackIndex = 0; // 공격할 몬스터 인덱스
        private Skills _selectedSkill = null; // 선택된 스킬 저장 필드 추가
        private bool _isSelectingSkillTarget = false; // 대상 선택이 스킬 때문인지 구분하는 플래그


        public BattleScene(Character player, IScene previousScene)
        {
            _player = player;
            _previousScene = previousScene;
            _monsters = new List<Monster>();
            _random = new Random();
            _currentState = BattleState.PlayerTurn_SelectAction; // 초기 상태: 플레이어 행동 선택
            _selectedSkill = null;
            _isSelectingSkillTarget = false;

            SpawnMonsters();
        }

        // 몬스터 랜덤 생성 (1~4마리)
        private void SpawnMonsters()
        {
            int monsterCount = _random.Next(1, 5); // 1에서 4 사이의 랜덤 수
            for (int i = 0; i < monsterCount; i++)
            {
                int monsterType = _random.Next(0, 3); // 0: 미니언, 1: 대포미니언, 2: 공허충
                switch (monsterType)
                {
                    case 0:
                        _monsters.Add(new Minion());
                        break;
                    case 1:
                        _monsters.Add(new CannonMinion());
                        break;
                    case 2:
                        _monsters.Add(new VoidBug());
                        break;
                }
            }
        }

        // 공통: 화면 상단 표시 (몬스터 정보, 플레이어 정보)
        private void DisplayBattleStatus(bool showMonsterNumbers = false)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            // 몬스터 정보 표시
            for (int i = 0; i < _monsters.Count; i++)
            {
                Monster monster = _monsters[i];
                string prefix = showMonsterNumbers && !monster.IsDead ? $"{i + 1}. " : ""; // 살아있는 몬스터에게만 번호 표시
                string monsterStatus = monster.IsDead ? "Dead" : $"HP {monster.CurrentHealth}";
                // 죽은 몬스터는 다른 방식으로 표시 (예: 괄호 추가 또는 색상 변경 - 여기서는 텍스트만)
                string displayInfo = $"{prefix}Lv.{monster.Level:D2} {monster.Name} {monsterStatus}";
                if (monster.IsDead)
                {

                    Console.WriteLine($"[DEAD] {displayInfo.TrimStart('0', '1', '2', '3', '4', '.', ' ')}"); // 번호 제거하고 [DEAD] 추가
                }
                else
                {
                    Console.WriteLine(displayInfo);
                }
            }
            Console.WriteLine();

            // 플레이어 정보 표시
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{_player.Level:D2} {_player.Name} ({_player.Job})");
            Console.WriteLine($"HP {_player.CurrentHealth}/{_player.MaxHealth}");
            Console.WriteLine($"MP {_player.CurrentMana}/{_player.MaxMana}");

            Console.WriteLine();
        }

        // IScene.Update 구현 (전투 상태에 따라 분기)
        public IScene Update()
        {
            switch (_currentState)
            {
                case BattleState.PlayerTurn_SelectAction:
                    return SelectAction();
                case BattleState.PlayerTurn_SelectTarget:
                    return SelectTarget();
                case BattleState.PlayerTurn_SelectSkill: //  스킬 선택
                    return SelectSkill();
                case BattleState.PlayerTurn_ShowResult:
                    return HandlePlayerAttackResult();
                case BattleState.EnemyTurn_ShowResult:
                    return HandleEnemyAttackResult();
                case BattleState.BattleOver_Victory:
                case BattleState.BattleOver_Lose:
                    return HandleBattleOver();
                default:
                    // 예외 처리 또는 기본 상태로 돌아가기
                    Console.WriteLine("알 수 없는 전투 상태입니다.");
                    Console.ReadKey();
                    return _previousScene; // 안전하게 이전 화면으로
            }
        }

        private IScene SelectSkill()
        {
            DisplayBattleStatus();
            for (int i = 0; i < _player.SkillList.Count; i++)
            {
                Skills skill = _player.SkillList[i];
                Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.MPCost}");
                Console.WriteLine($"   {skill.Description}");
            }
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.>> ");

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int skillIndex))
                {
                    if (skillIndex > 0 && skillIndex <= _player.SkillList.Count)
                    {
                        Skills currentSelectedSkill = _player.SkillList[skillIndex - 1]; // 지역 변수에 저장

                        if (_player.CurrentMana >= currentSelectedSkill.MPCost)
                        {
                            _selectedSkill = currentSelectedSkill; // 필드에 최종 저장

                            switch (_selectedSkill.Type)
                            {
                                case SkillType.SingleTarget:
                                    _currentState = BattleState.PlayerTurn_SelectTarget;
                                    _isSelectingSkillTarget = true; // 스킬 대상 선택 플래그 설정
                                    return this;
                                case SkillType.RandomTarget:
                                    UseSkill(_selectedSkill); // 대상 선택 없이 바로 사용
                                    _currentState = BattleState.PlayerTurn_ShowResult;
                                    _selectedSkill = null; // 사용 후 초기화
                                    return this;
                                default:
                                    Console.WriteLine("아직 지원되지 않는 스킬 타입입니다.");
                                    _selectedSkill = null; // 지원 안되므로 초기화
                                    _currentState = BattleState.PlayerTurn_SelectAction;
                                    return this;
                            }
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.");
                            Console.ReadKey();
                            _currentState = BattleState.PlayerTurn_SelectAction;
                            return this;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
                Console.Write(">> ");
            }
        }

        //private IScene select skills()
        //{
        //    DisplayBattleStatus(); // 기본 전투 정보 표시
        //    Console.WriteLine("1. 알파스 트라이크");
        //    Console.WriteLine("2. 더블 스트라이크");
        //    Console.WriteLine("3. 명상");

        //    Console.WriteLine();
        //    Console.Write("원하시는 행동을 입력해주세요.>> ");


        //    while (true)
        //    {
        //        string input = Console.ReadLine();
        //        switch (input)
        //        {
        //            case "1":
        //                Console.WriteLine("알파 스트라이크!");
        //                _currentState = BattleState.PlayerTurn_SelectTarget; // 공격 대상 선택 상태로 변경

        //                return this; // 다음 루프에서 HandlePlayerTargetSelection 호출

        //            case "2":
        //                Console.WriteLine("더블 스트라이크!");

        //                return this;

        //            default:
        //                Console.WriteLine("잘못된 입력입니다.");
        //                Console.Write(">> ");
        //                break;
        //        }

        //    }
        //}
        private void UseSkill(Skills skill, Monster target)
        {
            _player.CurrentMana -= skill.MPCost;
            // 데미지 계산 시 Skill 객체의 DamageMultiplier 사용
            int damage = (int)Math.Round(_player.Attack * skill.DamageMultiplier);

            Console.WriteLine();
            // 스킬 이름, MP 소모량 등 Skill 객체 정보 사용
            Console.WriteLine($"{_player.Name} 의 {skill.Name}! (MP {skill.MPCost} 소모)");
            Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 공격! [데미지 : {damage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{target.Level} {target.Name}");
            Console.Write($"HP {target.CurrentHealth} -> ");
            target.TakeDamage(damage);
            Console.WriteLine(target.IsDead ? "Dead" : $"{target.CurrentHealth}");
        }

        private void UseSkill(Skills skill)
        {
            _player.CurrentMana -= skill.MPCost;
            int damage = (int)Math.Round(_player.Attack * skill.DamageMultiplier);
            List<Monster> aliveMonsters = _monsters.Where(m => !m.IsDead).ToList();

            Console.WriteLine();
            Console.WriteLine($"{_player.Name} 의 {skill.Name}! (MP {skill.MPCost} 소모)");

            if (skill.Type == SkillType.RandomTarget)
            {
                // 대상 수도 Skill 객체의 NumberOfTargets 사용
                int targetCount = Math.Min(skill.NumberofTargets, aliveMonsters.Count);
                if (targetCount == 0)
                {
                    Console.WriteLine("공격할 대상이 없습니다.");
                    return;
                }

                List<Monster> targets = aliveMonsters.OrderBy(x => _random.Next()).Take(targetCount).ToList();
                foreach (Monster t in targets)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{t.Level} {t.Name} 을(를) 공격! [데미지 : {damage}]");
                    Console.WriteLine($"Lv.{t.Level} {t.Name}");
                    Console.Write($"HP {t.CurrentHealth} -> ");
                    t.TakeDamage(damage);
                    Console.WriteLine(t.IsDead ? "Dead" : $"{t.CurrentHealth}");
                }
            }
            // 다른 대상 지정 불필요 스킬 타입 추가 시 여기에 case 추가
        }




        // --- 각 상태 처리 메서드 (아래에 이어서 구현) ---
        private IScene SelectAction()
        {
            DisplayBattleStatus(); // 기본 전투 정보 표시
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");

            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.>> ");

            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _currentState = BattleState.PlayerTurn_SelectTarget; // 공격 대상 선택 상태로 변경
                        _isSelectingSkillTarget = false; // 일반 공격 대상 선택 
                        return this; // 다음 루프에서 HandlePlayerTargetSelection 호출

                    case "2":
                        _currentState = BattleState.PlayerTurn_SelectSkill;
                        return this;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.Write(">> ");
                        break;
                }
            }
        }
        private IScene SelectTarget()
        {
            DisplayBattleStatus(true);
            string purpose = _isSelectingSkillTarget ? $"[{_selectedSkill.Name}] 스킬 대상 선택" : "공격 대상 선택";
            Console.WriteLine(purpose);
            if (!_isSelectingSkillTarget)
            {
                Console.WriteLine("0. 취소");
            }
            Console.WriteLine();
            Console.Write("대상을 선택해주세요.>> ");

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int targetIndex))
                {
                    if (!_isSelectingSkillTarget && targetIndex == 0)
                    {
                        _currentState = BattleState.PlayerTurn_SelectAction;
                        _selectedSkill = null;
                        _isSelectingSkillTarget = false;
                        return this;
                    }
                    if (_isSelectingSkillTarget && targetIndex == 0)
                    {
                        Console.WriteLine("잘못된 입력입니다. 스킬 대상 선택 중에는 취소할 수 없습니다.");
                        Console.Write(">> ");
                        continue;
                    }

                    if (targetIndex > 0 && targetIndex <= _monsters.Count)
                    {
                        Monster targetMonster = _monsters[targetIndex - 1];
                        if (!targetMonster.IsDead)
                        {
                            if (_isSelectingSkillTarget)
                            {
                                // 스킬 대상 선택 완료 -> UseSkill 호출
                                UseSkill(_selectedSkill, targetMonster);
                            }
                            else
                            {
                                // 일반 공격 대상 선택 완료 -> PlayerAttack 호출
                                PlayerAttack(targetMonster);
                            }
                            _currentState = BattleState.PlayerTurn_ShowResult;
                            _selectedSkill = null; // 행동 후 선택된 스킬 초기화
                            _isSelectingSkillTarget = false; // 플래그 초기화
                            return this;
                        }
                        else
                        {
                            Console.WriteLine("이미 죽은 몬스터입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
                Console.Write(">> ");
            }
        }



        //private IScene SelectTarge()
        //{
        //    DisplayBattleStatus(true); // 몬스터 번호 표시
        //    Console.WriteLine("0. 취소");
        //    Console.WriteLine();
        //    Console.Write("대상을 선택해주세요.>> ");

        //    while (true)
        //    {
        //        string input = Console.ReadLine();
        //        if (int.TryParse(input, out int targetIndex))
        //        {
        //            if (targetIndex == 0)
        //            {
        //                _currentState = BattleState.PlayerTurn_SelectAction; // 행동 선택으로 복귀
        //                return this;
        //            }
        //            // 유효한 인덱스인지 확인 (1부터 시작하므로 -1)
        //            if (targetIndex > 0 && targetIndex <= _monsters.Count)
        //            {
        //                Monster targetMonster = _monsters[targetIndex - 1];
        //                if (!targetMonster.IsDead)
        //                {
        //                    // 몬스터 공격 및 결과 표시 상태로 전환
        //                    PlayerAttack(targetMonster);
        //                    _currentState = BattleState.PlayerTurn_ShowResult;
        //                    return this;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("이미 죽은 몬스터입니다.");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("잘못된 입력입니다.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("숫자를 입력해주세요.");
        //        }
        //        Console.Write(">> ");
        //    }
        //}
        private IScene HandlePlayerAttackResult()
        {
            // 공격 결과는 PlayerAttack 메서드에서 이미 출력됨
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.Write(">> ");

            while (Console.ReadLine() != "0")
            {
                Console.WriteLine("잘못된 입력입니다. 0을 입력하여 다음 턴으로 진행하세요.");
                Console.Write(">> ");
            }

            // 전투 종료 조건 확인
            if (_monsters.All(monster => monster.IsDead))
            {
                _currentState = BattleState.BattleOver_Victory;

                return this;
            }
            if (!_player.IsAlive)
            {
                _currentState = BattleState.BattleOver_Lose;
                return this;
            }

            // 몬스터 턴 시작
            _enemyAttackIndex = 0; // 첫 번째 몬스터부터 공격 시작
            _currentState = BattleState.EnemyTurn_ShowResult;
            return this;
        }
        private IScene HandleEnemyAttackResult()
        {
            // 모든 몬스터가 공격했거나, 플레이어가 죽었으면 플레이어 턴으로
            if (_enemyAttackIndex >= _monsters.Count || !_player.IsAlive)
            {
                _currentState = BattleState.PlayerTurn_SelectAction;
                return this;
            }

            _currentAttackingMonster = _monsters[_enemyAttackIndex];

            // 현재 몬스터가 살아있으면 공격
            if (!_currentAttackingMonster.IsDead)
            {
                DisplayBattleStatus(); // 공격 전에 현재 상태 보여주기
                Console.WriteLine("--- Enemy Phase ---");
                _currentAttackingMonster.AttackPlayer(_player); // Monster 클래스의 공격 메서드 사용
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.Write(">> ");

                while (Console.ReadLine() != "0")
                {
                    Console.WriteLine("잘못된 입력입니다. 0을 입력하여 진행하세요.");
                    Console.Write(">> ");
                }

                // 플레이어 사망 체크
                if (!_player.IsAlive)
                {
                    _currentState = BattleState.BattleOver_Lose;
                    return this;
                }
            }

            _enemyAttackIndex++; // 다음 몬스터로 인덱스 증가
            return this; // 다음 몬스터 공격 또는 플레이어 턴으로 전환
        }
        private IScene HandleBattleOver()
        {
            Console.Clear();
            Console.WriteLine("===Battle!! - Result===");
            Console.WriteLine();

            int initialHp = _player.MaxHealth; // 전투 시작 시 체력 (정확하려면 전투 시작 시 저장해야 함, 여기선 Max로 가정)
            // TODO: 전투 시작 시 플레이어의 초기 체력을 저장해두는 로직 추가 필요

            if (_currentState == BattleState.BattleOver_Victory)
            {
                Console.WriteLine("■□■□■□■□■□");
                Console.WriteLine("□    Victory     ■");
                Console.WriteLine("■□■□■□■□■□");
                Console.WriteLine();
                int defeatedMonsters = _monsters.Count(m => m.IsDead); // 처치한 몬스터 수
                Console.WriteLine($"던전에서 몬스터 {defeatedMonsters}마리를 잡았습니다.");
            }
            else // BattleState.BattleOver_Lose
            {
                Console.WriteLine("You Lose You Lose You Lose You Lose \n You Lose You Lose You Lose You Lose ");
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{_player.Level:D2} {_player.Name}");
            // Console.WriteLine($"HP {initialHp} -> {_player.CurrentHealth}"); // 초기 체력 필요
            Console.WriteLine($"HP {_player.MaxHealth} -> {_player.CurrentHealth}"); // 임시로 MaxHealth 사용


            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.Write(">> ");

            while (Console.ReadLine() != "0")
            {
                Console.WriteLine("잘못된 입력입니다. 0을 입력하여 로비로 돌아가세요.");
                Console.Write(">> ");
            }

            // 전투 종료 후 이전 Scene (MainScene)으로 돌아감
            // Player 상태가 BattleScene에서 변경되었으므로, 업데이트된 Player 객체로 MainScene 재생성
            return new EndingScene();
        }

        // 플레이어 공격 로직 (데미지 계산 포함)
        private void PlayerAttack(Monster target)
        {
            int baseAttack = _player.Attack;
            int errorMargin = (int)Math.Ceiling(baseAttack * 0.1);

            //  기본 데미지 계산
            int normalDamage = _random.Next(baseAttack - errorMargin, baseAttack + errorMargin + 1);

            //  회피 기능
            bool isDodged = _random.Next(100) < 10; //10퍼로 회피함
            if (isDodged)
            {
                Console.WriteLine();
                Console.WriteLine($"{_player.Name} 의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name} 이(가) 공격을 회피했습니다!");
                Console.WriteLine();

                _lastAttackedMonster = target;
                _lastDamageDealt = 0;
                return;
            }

            //  치명타 여부
            bool isCritical = _random.Next(100) < 15; // 15% 확률

            //  최종 데미지 계산
            int criticalBonus = isCritical ? (int)Math.Round(normalDamage * 0.6) : 0;
            int finalDamage = normalDamage + criticalBonus;

            //  출력
            Console.WriteLine();
            Console.WriteLine($"{_player.Name} 의 공격!");
            if (isCritical)
            {
                Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]" + (isCritical ? " - 치명타 공격!!" : ""));
            }
            else
            {
                Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]");
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{target.Level} {target.Name}");
            Console.Write($"HP {target.CurrentHealth} -> ");
            target.TakeDamage(finalDamage);
            Console.WriteLine(target.IsDead ? "Dead" : $"{target.CurrentHealth}");

            _lastAttackedMonster = target;
            _lastDamageDealt = finalDamage;



        }
    }
}

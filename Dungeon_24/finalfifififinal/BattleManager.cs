using System;
using System.Collections.Generic;
using System.Linq;

namespace SpartaDungeon
{
    public class BattleManager
    {
        private Player player;
        private Random random = new Random();
        private List<Monster> monsterList = new List<Monster>();
        private ExpSet expSet = new ExpSet();
        private EnemyPhaseManager enemyPhaseManager = new EnemyPhaseManager();
        private BattleResultChecker resultChecker = new BattleResultChecker();

        public BattleManager(Player player)
        {
            this.player = player;
        }

        public void StartBattle()
        {
            monsterList = GenerateRandomMonsters();
            player.PreviousHealth = player.Health;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Battle!! =====\n");

                for (int i = 0; i < monsterList.Count; i++)
                {
                    Monster m = monsterList[i];
                    string status = m.IsDead ? "Dead" : $"HP {m.Health}";
                    Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name} {status}");
                }

                Console.WriteLine("\n[내정보]");
                Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Health}/{player.MaxHealth}");
                Console.WriteLine($"EXP {player.Exp}/{player.MaxExp}");
                Console.WriteLine($"GOLD: {player.Gold} G");

                Console.WriteLine("\n0. 취소");
                Console.WriteLine("\n대상을 선택해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > monsterList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르세요.");
                    Console.ReadKey();
                    continue;
                }
                if (choice == 0)
                    break;

                Monster target = monsterList[choice - 1];
                if (target.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다. 아무 키나 누르세요.");
                    Console.ReadKey();
                    continue;
                }

                int damage = player.CalculateAttackDamage();

                Console.WriteLine($"{player.Name} 의 공격!");
                Console.WriteLine($"{target.Name} 을(를) 맞췄습니다.  [데미지 : {damage}]");

                int beforeHP = target.Health;
                target.Health -= damage;

                if (target.Health <= 0)
                {
                    target.Health = 0;
                    target.IsDead = true;
                    Console.WriteLine($"Lv.{target.Level} {target.Name}HP {beforeHP} -> Dead");
                    expSet.GainRewards(player, target);
                }
                else
                {
                    Console.WriteLine($"Lv.{target.Level} {target.Name}HP {beforeHP} -> {target.Health}");
                }

                Console.WriteLine("0. 다음");
                Console.ReadLine();

                Console.WriteLine("==== 몬스터의 공격 차례입니다 ====");
                enemyPhaseManager.Execute(player, monsterList);

                if (CheckBattleEnd())
                    break;

                Console.WriteLine("플레이어의 차례로 돌아갑니다. 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }

        private bool CheckBattleEnd()
        {
            if (resultChecker.IsPlayerDefeated(player))
            {
                Console.Clear();
                Console.WriteLine("===========================");
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("===========================");

                Console.WriteLine("Defeat");
                Console.WriteLine("던전에서 쓰러졌습니다...");

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.PreviousHealth} -> {player.Health}");

                expSet.ApplyPenalty();
                expSet.PrintTotalRewards();

                Console.WriteLine("0. 다음");
                Console.Write(">> ");
                Console.ReadLine();
                player.Health = 100;
                return true;
            }

            if (resultChecker.AreAllMonstersDefeated(monsterList))
            {
                Console.Clear();
                Console.WriteLine("===========================");
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("===========================");

                Console.WriteLine("Victory");
                int defeatedCount = monsterList.Count(m => m.IsDead);
                Console.WriteLine($"던전에서 몬스터 {defeatedCount}마리를 잡았습니다.");

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.PreviousHealth} -> {player.Health}");

                expSet.PrintTotalRewards();

                Console.WriteLine("0. 다음");
                Console.Write(">> ");
                Console.ReadLine();
                return true;
            }

            return false;
        }

        private List<Monster> GenerateRandomMonsters()
        {
            List<Monster> list = new List<Monster>();

            for (int i = 0; i < 3; i++)
            {
                int type = random.Next(3);
                Monster m = null;
                switch (type)
                {
                    case 0:
                        m = new Minion();
                        break;
                    case 1:
                        m = new Voidbug();
                        break;
                    case 2:
                        m = new CannonMinion();
                        break;
                }
                m.InitStats();
                list.Add(m);
            }

            return list;
        }
    }
}

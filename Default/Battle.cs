using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaDungeon;

namespace ConsoleApp8
{
    public class Battle
    {
        Player player = Program.player;
        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("\nBattle!!");
            MonsterFactory.ShowMonster();
            Console.WriteLine();
            player.ShowMe();

            while (true)
            {
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine().Trim();
                if (input == "0")
                {
                    MonsterFactory.MonsterGroup.Clear();
                    break;
                }
                else if (input == "1")
                {
                    player.OriginHP = player.Health;
                    InBattle();
                    MonsterFactory.MonsterGroup.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Pause();
                }
            }
        }

        void InBattle()                                                 //몬스터가 죽었다면 해당 몬스터에 텍스트는 전부 어두운 색으로 표시합니다.
        {
            bool isloop = true;

            while (isloop)
            {
                Console.Clear();
                Console.WriteLine("\nBattle!!\n");
                MonsterFactory.Encounter();
                player.ShowMe();

                Console.WriteLine("0. 취소\n");
                Console.Write(">>");

                bool isAllDead = MonsterFactory.MonsterGroup.All(x => x.IsDead == true);

                if (isAllDead == true || player.IsDeadP == true)
                {
                    BattleResult();
                    break;
                }

                string input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "0":
                        isloop = false;
                        break;
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        Monster monster = MonsterFactory.MonsterGroup[int.Parse(input) - 1];

                        if (monster.IsDead == false)
                        {
                            player.AttackedDamage = player.AttackDamage();
                            monster.MonsterBeforeHP = monster.MonsterHP;
                            monster.MonsterHP -= player.AttackedDamage;

                            if (monster.MonsterHP <= 0)
                            {
                                monster.MonsterHP = 0;
                                monster.IsDead = true;
                            }
                            BattleDamage(monster);
                            MonsterAttack();

                            //player.AttackedHealth = player.Health;
                        }
                        else
                        {
                            Console.WriteLine("\n잘못된 입력입니다.\n");
                            Pause();
                        }
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.\n");
                        Pause();
                        break;
                }
            }
        }

        void BattleDamage(Monster monster)
        {
            //Monster monster = MonsterFactory.MonsterGroup[int.Parse(input) - 1];
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nBattle!!\n");
                Console.WriteLine($"{player.Name} 의 공격!");
                Console.WriteLine($"Lv.{monster.MonsterLv} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 : {player.AttackedDamage}]");
                Console.WriteLine($"\nLv.{monster.MonsterLv} {monster.MonsterName}");
                string monsterStatus = monster.IsDead ? "Dead" : $"HP {monster.MonsterHP}";
                Console.WriteLine($"HP {monster.MonsterBeforeHP} -> {monsterStatus}\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">>");
                string input = Console.ReadLine().Trim();
                if (input == "0")
                    break;
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Pause();
                }
            }
        }

        void MonsterAttack()
        {
            for (int i = 0; i < MonsterFactory.MonsterGroup.Count; i++)
            {
                Monster monster = MonsterFactory.MonsterGroup[i];

                if (monster.IsDead == false)
                {
                    player.AttackedHealth = player.Health;
                    player.Health -= monster.MonsterAtt;

                    if (player.Health <= 0)
                    {
                        player.Health = 0;
                        player.IsDeadP = true;
                    }

                    MonsterBattleDamage(monster);
                }
            }
        }

        void MonsterBattleDamage(Monster monster)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nBattle!!\n");
                Console.WriteLine($"Lv.{monster.MonsterLv} {monster.MonsterName} 의 공격!");
                Console.WriteLine($"Lv.{player.Name} 을(를) 맞췄습니다. [데미지 : {monster.MonsterAtt}]");
                Console.WriteLine($"\nLv.{player.Level} {player.Name}");
                string playerStatus = player.IsDeadP ? "Dead" : $"HP {player.Health}";
                Console.WriteLine($"HP {player.AttackedHealth} -> {playerStatus}\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">>");
                string input = Console.ReadLine().Trim();
                if (input == "0")
                    break;
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Pause();
                }
            }
        }

        void BattleResult()
        {
            while (true)
            {
                if (player.IsDeadP != true)
                {
                    Console.Clear();
                    Console.WriteLine("\nBattle!! - Result\n");
                    Console.WriteLine("Victory");
                    Console.WriteLine($"\n던전에서 몬스터 {MonsterFactory.MonsterGroup.Count}마리를 잡았습니다.\n");
                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    Console.WriteLine($"HP {player.OriginHP} -> {player.Health}\n");
                    Console.WriteLine("0. 다음\n");
                    Console.Write(">>");
                    string input = Console.ReadLine().Trim();
                    if (input == "0")
                        break;
                    else
                    {
                        Console.WriteLine("\n잘못된 입력입니다.\n");
                        Pause();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nBattle!! - Result\n");
                    Console.WriteLine("Defeat\n");
                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    Console.WriteLine($"HP {player.OriginHP} -> {player.Health}\n");
                    Console.WriteLine("0. 다음\n");
                    Console.Write(">>");
                    string input = Console.ReadLine().Trim();
                    if (input == "0")
                        break;
                    else
                    {
                        Console.WriteLine("\n잘못된 입력입니다.\n");
                        Pause();
                    }
                }
            }
        }
        void Pause()
        {
            Console.WriteLine("\n아무 키나 누르면 계속합니다...");
            Console.ReadKey();
        }
    }
}

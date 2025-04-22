using System;
using System.Collections.Generic;

namespace SpartaDungeon
{
    public class BattleManager
    {
        private Player player;
        private Random random = new Random();
        private List<Monster> monsterList = new List<Monster>();

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
                Console.WriteLine($"HP {player.Health}/{player.Health}");

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

                double offset = Math.Ceiling(player.Attack * 0.1);
                int damage = player.Attack + random.Next(-(int)offset, (int)offset + 1);
                if (damage < 0) damage = 0;

                Console.WriteLine($"\n{player.Name} 의 공격!");
                Console.WriteLine($"{target.Name} 을(를) 맞췄습니다.  [데미지 : {damage}]");

                int beforeHP = target.Health;
                target.Health -= damage;

                if (target.Health <= 0)
                {
                    target.Health = 0;
                    target.IsDead = true;
                    Console.WriteLine($"Lv.{target.Level} {target.Name}\nHP {beforeHP} -> Dead");
                }
                else
                {
                    Console.WriteLine($"Lv.{target.Level} {target.Name}\nHP {beforeHP} -> {target.Health}");
                }

                Console.WriteLine("\n0. 다음");
                Console.ReadLine();

                // ===== Enemy Phase =====
                Console.WriteLine("\n===== Enemy Phase =====\n");
                foreach (Monster m in monsterList)
                {
                    if (m.IsDead) continue;

                    Console.WriteLine($"Lv.{m.Level} {m.Name} 의 공격!");
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.  [데미지 : {m.Attack}]");

                    int before = player.Health;
                    player.Health -= m.Attack;
                    if (player.Health < 0) player.Health = 0;

                    Console.WriteLine($"Lv.{player.Level} {player.Name}\nHP {before} -> {player.Health}\n");
                    Console.WriteLine("0. 다음");
                    Console.ReadLine();
                }

                Console.WriteLine("\n플레이어의 차례로 돌아갑니다. 아무 키나 누르세요.");
                Console.ReadKey();
            }
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

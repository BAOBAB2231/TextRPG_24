using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using TextRPG_24_J;

namespace TextRPG_24_J
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public bool IsDead => Hp <= 0;

        public Monster(string name, int level, int hp, int atk)
        {
            Name = name;
            Level = level;
            MaxHp = hp;
            Hp = hp;
            Attack = atk;
        }
    }

    public static class Battle
    {
        static List<Monster> monsters = new();
        static Random random = new();

        public static void Show(Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // 몬스터 생성
            monsters.Clear();
            int count = random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int type = random.Next(3);
                switch (type)
                {
                    case 0:
                        monsters.Add(new Monster("미니언", 2, 15, 5));
                        break;
                    case 1:
                        monsters.Add(new Monster("공허충", 3, 10, 9));
                        break;
                    case 2:
                        monsters.Add(new Monster("대포미니언", 5, 25, 8));
                        break;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");

                // 몬스터 목록 표시
                for (int i = 0; i < monsters.Count; i++)
                {
                    Monster m = monsters[i];
                    if (m.IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  HP {m.Hp}");
                    }
                }

                Console.WriteLine("\n[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Health}/100\n");

                Console.WriteLine("1. 공격");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    // 대상 선택
                    Console.WriteLine("\n대상을 선택해주세요.");
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {monsters[i].Name} {(monsters[i].IsDead ? "(Dead)" : "")}");
                    }
                    Console.WriteLine("0. 취소");
                    Console.Write(">> ");
                    string select = Console.ReadLine();

                    if (select == "0") continue;
                    if (!int.TryParse(select, out int targetIndex) || targetIndex < 1 || targetIndex > monsters.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        continue;
                    }

                    Monster target = monsters[targetIndex - 1];
                    if (target.IsDead)
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다.");
                        Thread.Sleep(1000);
                        continue;
                    }

                    // 공격
                    int variation = (int)Math.Ceiling(player.Attack * 0.1);
                    int finalDamage = random.Next(player.Attack - variation, player.Attack + variation + 1);
                    target.Hp -= finalDamage;
                    if (target.Hp < 0) target.Hp = 0;
                    Console.WriteLine($"{target.Name}을(를) 공격했습니다! [데미지 : {finalDamage}]");
                    Thread.Sleep(1000);

                    // 몬스터가 다 죽었으면 Victory
                    if (monsters.All(m => m.IsDead))
                    {
                        Console.Clear();
                        Console.WriteLine("Battle!! - Result\n");
                        Console.WriteLine("Victory\n");
                        Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");
                        Console.WriteLine($"Lv.{player.Level} {player.Name}\nHP 100 -> {player.Health}\n");
                        Console.WriteLine("0. 다음\n>> ");
                        Console.ReadLine();
                        return;
                    }

                    // 몬스터 턴
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        var m = monsters[i];
                        if (m.IsDead) continue;

                        Console.Clear();
                        Console.WriteLine("Battle!!\n");
                        Console.WriteLine($"{m.Name}의 공격!\n{player.Name}을(를) 맞췄습니다.");
                        int monVar = (int)Math.Ceiling(m.Attack * 0.1);
                        int dmg = random.Next(m.Attack - monVar, m.Attack + monVar + 1);
                        int prevHp = player.Health;
                        player.Health -= dmg;
                        if (player.Health < 0) player.Health = 0;
                        Console.WriteLine($"[데미지 : {dmg}]\n");
                        Console.WriteLine($"Lv.{player.Level} {player.Name}\nHP {prevHp} -> {player.Health}\n");
                        Console.WriteLine("0. 다음\n>> ");
                        Console.ReadLine();

                        if (player.Health <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Battle!! - Result\n");
                            Console.WriteLine("You Lose\n");
                            Console.WriteLine($"Lv.{player.Level} {player.Name}\nHP 100 -> 0\n");
                            Console.WriteLine("0. 다음\n>> ");
                            Console.ReadLine();
                            return;
                        }
                    }
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TextRPG_24_J
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int ExpReward { get; set; }     // 경험치 보상
        public int GoldReward { get; set; }    // 골드 보상
        public bool IsDead => Hp <= 0;

        public Monster(string name, int level, int hp, int atk, int expReward, int goldReward)
        {
            Name = name;
            Level = level;
            MaxHp = hp;
            Hp = hp;
            Attack = atk;
            ExpReward = expReward;
            GoldReward = goldReward;
        }
    }

    public static class Battle
    {
        static List<Monster> monsters = new();
        static Random random = new();
        static int totalGold = 0;  // 전투 중 획득한 총 골드

        public static void Show(Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");

            monsters.Clear();
            totalGold = 0;
            int count = random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int type = random.Next(3);
                switch (type)
                {
                    case 0:
                        monsters.Add(new Monster("미니언", 2, 15, 5, 10, random.Next(220, 450)));
                        break;
                    case 1:
                        monsters.Add(new Monster("공허충", 3, 10, 9, 20, random.Next(420, 650)));
                        break;
                    case 2:
                        monsters.Add(new Monster("대포미니언", 5, 25, 8, 30, random.Next(620, 850)));
                        break;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");

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
                        Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  HP {m.Hp}  ATK {m.Attack}");
                    }
                }

                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.HP}/100");
                Console.WriteLine($"MP {player.CurrentMana}/50");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.Write("원하시는 행동을 입력해주세요.>> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("대상을 선택해주세요.");
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

                    int variation = (int)Math.Ceiling(player.Attack * 0.1);
                    int finalDamage = random.Next(player.Attack - variation, player.Attack + variation + 1);
                    target.Hp -= finalDamage;
                    if (target.Hp < 0) target.Hp = 0;
                    Console.WriteLine($"{target.Name}을(를) 공격했습니다! [데미지 : {finalDamage}]");
                    Thread.Sleep(1000);

                    if (target.IsDead)
                    {
                        Console.WriteLine($"{target.Name} 처치! 경험치 +{target.ExpReward}, 골드 +{target.GoldReward}G");
                        LevelUpManager.AddExp(player, target.ExpReward);
                        totalGold += target.GoldReward;
                        Thread.Sleep(1000);
                    }

                    if (monsters.All(m => m.IsDead))
                    {
                        player.Gold += totalGold;
                        Console.Clear();
                        Console.WriteLine("Battle!! - Result");
                        Console.WriteLine("Victory");
                        Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
                        Console.WriteLine($"획득 골드: {totalGold}G");
                        Console.WriteLine($"보유 골드: {player.Gold}G");
                        Console.WriteLine("0. 다음>> ");
                        Console.ReadLine();
                        return;
                    }

                    for (int i = 0; i < monsters.Count; i++)
                    {
                        var m = monsters[i];
                        if (m.IsDead) continue;

                        Console.Clear();
                        Console.WriteLine("Battle!!");
                        Console.WriteLine($"{m.Name}의 공격!{player.Name}을(를) 맞췄습니다.");

                        int monVar = (int)Math.Ceiling(m.Attack * 0.1);
                        int rawDmg = random.Next(m.Attack - monVar, m.Attack + monVar + 1);
                        int actualDmg = rawDmg - player.Defense;
                        if (actualDmg < 0) actualDmg = 0;

                        int prevHp = player.HP;
                        player.HP -= actualDmg;
                        if (player.HP < 0) player.HP = 0;

                        Console.WriteLine($"[데미지 : {actualDmg}] (원래 공격력: {rawDmg}, 방어력: {player.Defense})");
                        Console.WriteLine($"Lv.{player.Level} {player.Name}HP {prevHp} -> {player.HP}");

                        Console.WriteLine("0. 다음>> ");
                        Console.ReadLine();
                        if (player.HP <= 0)
                        {
                            totalGold = (int)(totalGold * 0.8);
                            player.Gold += totalGold;

                            Console.Clear();
                            Console.WriteLine("Battle!! - Result");
                            Console.WriteLine("You Lose");
                            Console.WriteLine($"획득 골드 (패배 보정): {totalGold}G");
                            Console.WriteLine($"보유 골드: {player.Gold}G");
                            Console.WriteLine($"Lv.{player.Level} {player.Name}HP 100 -> 0");
                            Console.WriteLine("0. 다음>> ");
                            Console.ReadLine();
                            return;
                        }
                    }
                }
                else if (input == "2")
                {
                    Console.WriteLine("스킬 기능은 아직 구현되지 않았습니다.");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

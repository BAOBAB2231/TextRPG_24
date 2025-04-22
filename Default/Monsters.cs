using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    enum MonsterType
    {
        NormalType = 0,
        RareType = 1,
        BossType = 2
    }

    internal class MonsterShell
    {
        //몬스터 정보 프로퍼티
        public string MonsterName { get; set; }
        public int MonsterHP { get; set; }
        public int MonsterLv { get; set; }
        public int MonsterAtt { get; set; }
        public MonsterType MonsterType { get; set; }
        public bool isDead { get; set; }
    }

    internal class Monster
    {
        //몬스터 생성
        public static List<MonsterShell> MonsterGroup = new List<MonsterShell>();
        public static void MakeMonsters(int x)
        {
            switch (x)
            {
                case 1:
                    MonsterGroup.Add(new MonsterShell
                    {
                        MonsterName = "미니언",
                        MonsterHP = 15,
                        MonsterLv = 2,
                        MonsterAtt = 6,
                        MonsterType = MonsterType.NormalType
                    });
                    break;

                case 2:
                    MonsterGroup.Add(new MonsterShell
                    {
                        MonsterName = "대포미니언",
                        MonsterHP = 25,
                        MonsterLv = 5,
                        MonsterAtt = 8,
                        MonsterType = MonsterType.NormalType
                    });
                    break;

                case 3:
                    MonsterGroup.Add(new MonsterShell
                    {
                        MonsterName = "공허충",
                        MonsterHP = 10,
                        MonsterLv = 3,
                        MonsterAtt = 9,
                        MonsterType = MonsterType.NormalType
                    });
                    break;

                default:
                    break;
            }
        }

        public static void MonsterGrouping()
        {
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = rand.Next(0,4);
                int zeroCount = 0;
                const int MAXIMUM_ZERO = 3;

                if (randomNumber > 0)
                {
                    MakeMonsters(randomNumber);
                }
                else
                {
                    if (zeroCount >= MAXIMUM_ZERO)
                    {
                        randomNumber = rand.Next(1, 4);
                        MakeMonsters(randomNumber);
                    }
                    zeroCount++;
                }
            }
        }

        public static void ShowMonster()
        {
            MonsterGrouping();
            for (int i = 0; i < MonsterGroup.Count; i++)
            {
                MonsterShell monster = MonsterGroup[i];
                Console.WriteLine($"Lv.{monster.MonsterLv} {monster.MonsterName} HP {monster.MonsterHP}");
            }
        }

        public static void Encounter()
        {
            for (int i = 0; i < MonsterGroup.Count; i++)
            {
                MonsterShell monster = MonsterGroup[i];
                Console.WriteLine($"{i+1} Lv.{monster.MonsterLv} {monster.MonsterName} HP {monster.MonsterHP}");
            }
        }
    }
}

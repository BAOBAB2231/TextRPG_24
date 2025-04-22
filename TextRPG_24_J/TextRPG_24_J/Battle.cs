


namespace TextRPG_24_J
{
    class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }

        public Monster(string name, int level, int hp, int atk)
        {
            Name = name;
            Level = level;
            HP = hp;
            ATK = atk;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Lv.{Level} {Name}  HP {HP}");
        }
    }

    class Battle
    {
        static List<Monster> monsterTypes = new List<Monster>()
        {
            new Monster("미니언", 2, 15, 5),
            new Monster("공허충", 3, 10, 9),
            new Monster("대포미니언", 5, 25, 8)
        };

        public static void Show(Player player)
        {
            Console.Clear();

            // 1~4마리 몬스터 랜덤 등장
            Random rand = new Random();
            int monsterCount = rand.Next(1, 5);

            List<Monster> battleMonsters = new List<Monster>();
            for (int i = 0; i < monsterCount; i++)
            {
                int index = rand.Next(monsterTypes.Count);
                Monster selected = monsterTypes[index];
                battleMonsters.Add(new Monster(selected.Name, selected.Level, selected.HP, selected.ATK));
            }

            Console.WriteLine("Battle!!\n");
            foreach (var m in battleMonsters)
            {
                m.ShowInfo();
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Health}/{player.Health}\n");

            Console.WriteLine("1. 공격");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            Console.ReadKey();
        }
    }
}
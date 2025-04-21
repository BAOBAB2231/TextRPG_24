using System;

namespace SpartaDungeon
{
    class Program
    {
        // 플레이어 정보 클래스
        class Player
        {
            public string Name { get; set; } = "Chad";
            public string Job { get; set; } = "전사";
            public int Level { get; set; } = 1;
            public int Attack { get; set; } = 10;
            public int Defense { get; set; } = 5;
            public int Health { get; set; } = 100;
            public int Gold { get; set; } = 1500;

            public void ShowStatus()
            {
                Console.WriteLine("\n상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine($"Lv.{Level}");
                Console.WriteLine($"{Name}({Job})");
                Console.WriteLine($"공격력 : {Attack}");
                Console.WriteLine($"방어력 : {Defense}");
                Console.WriteLine($"체력 : {Health}");
                Console.WriteLine($"Gold : {Gold}G\n");
            }
        }

        static Player player = new Player();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작\n");
                Console.Write("원하시는 행동을 입력해 주세요: ");

                string input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    StartBattle();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Pause();
                }
            }
        }

        static void ShowStatus()
        {
            Console.Clear();
            player.ShowStatus();

            while (true)
            {
                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine().Trim();
                if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                }
            }
        }

        static void StartBattle()
        {
            Console.WriteLine("\n[전투를 시작합니다!]");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\n아무 키나 누르면 계속합니다...");
            Console.ReadKey();
        }
    }
}

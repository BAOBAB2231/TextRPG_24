using System;

namespace SpartaDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            BattleManager battleManager = new BattleManager(player);

            Console.WriteLine("===== Sparta Dungeon =====");
            Console.WriteLine("1. 던전 입장");
            Console.WriteLine("2. 상태 보기");
            Console.WriteLine("0. 종료");
            Console.Write("> ");

            string input = Console.ReadLine();
            while (input != "0")
            {
                switch (input)
                {
                    case "1":
                        battleManager.StartBattle();
                        break;
                    case "2":
                        player.ShowStatus();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }

                Console.WriteLine("===== Sparta Dungeon =====");
                Console.WriteLine("1. 던전 입장");
                Console.WriteLine("2. 상태 보기");
                Console.WriteLine("0. 종료");
                Console.Write("> ");
                input = Console.ReadLine();
            }

            Console.WriteLine("게임을 종료합니다.");
        }
    }
}

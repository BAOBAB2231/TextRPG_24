using System;
namespace SpartaDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            BattleManager battleManager = new BattleManager(player);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================");
                Console.WriteLine("🏰   스파르타 마을   🏰");
                Console.WriteLine("===========================\n");
                Console.WriteLine("[1] 던전 입장");
                Console.WriteLine("[2] 상태 보기");
                Console.WriteLine("[0] 게임 종료\n");
                Console.Write("원하는 행동을 선택하세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.Clear();
                    player.ShowStatus();
                    Console.WriteLine("\nEnter 키를 누르면 돌아갑니다.");
                    Console.ReadLine();
                }
                else if (input == "2")
                {
                    battleManager.StartBattle();
                }
                else if (input == "0")
                {
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. Enter 키를 누르세요.");
                    Console.ReadLine();
                }
            }
        }
    }
}
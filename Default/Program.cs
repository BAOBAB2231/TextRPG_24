using System;
using ConsoleApp8;

namespace SpartaDungeon
{
    public class Program
    {
        public static ItemFactory item = new ItemFactory();
        public static Player player = new Player();

        public static QuestBoard board = new QuestBoard(item, player);
        public static QuestUI questUI = new QuestUI(board, item);
        public static Battle battle = new Battle(player, questUI);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("3. 퀘스트 보기\n");

                Console.Write("원하시는 행동을 입력해 주세요: ");

                string input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    battle.StartBattle();
                }
                else if (input == "3")
                {
                    questUI.QuestMenu();
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
                    Pause();
                }
            }
        }


        static void Pause()
        {
            Console.WriteLine("\n아무 키나 누르면 계속합니다...");
            Console.ReadKey();
        }
    }
}

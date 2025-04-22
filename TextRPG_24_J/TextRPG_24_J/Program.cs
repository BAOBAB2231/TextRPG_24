namespace TextRPG_24_J
{
    
        class Program
        {
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

                    switch (input)
                    {
                        case "1":
                            ShowStatus();
                            break;
                        case "2":
                            Battle.Show(player);
                            break;
                        default:
                            Console.WriteLine("\n잘못된 입력입니다.");
                            Pause();
                            break;
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

            static void Pause()
            {
                Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                Console.ReadKey();
            }
        }
}


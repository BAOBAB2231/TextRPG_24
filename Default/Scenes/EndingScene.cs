using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Scenes
{
    public class EndingScene : IScene
    {
        public IScene Update()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine("=                  =");
            Console.WriteLine("=   Text RPG END   =");
            Console.WriteLine("=                  =");
            Console.WriteLine("====================");

            Console.WriteLine();
            Console.Write("원하는 작업을 선택하세요: ");
            Console.WriteLine("1. 처음으로");
            Console.WriteLine("아무키나 입력하여 종료하기");
            string input = Console.ReadLine();

            switch (input)
            {

                case "1":
                    // MainScene으로 전환

                    return new TitleScene();

                default:
                    Console.WriteLine("게임을 종료합니다.");
                    return null;
                    Console.ReadKey();
            }

        }


    }
}

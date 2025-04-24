using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Scenes
{
    public class TitleScene : IScene
    {
        public IScene Update()
        {
            Console.Clear(); // 화면 지우기
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#         텍스트 RPG         #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            Console.WriteLine();
            Console.WriteLine("1. 시작하기");
            Console.WriteLine("2. 종료하기");
            Console.WriteLine();
            Console.Write("원하는 작업을 선택하세요>>  ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // CharacterScene으로 전환

                    return new CharacterScene();

                case "2":
                    // 게임 종료 (null 반환)
                    Console.WriteLine("게임을 종료합니다.");
                    return null;
                default:
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 눌러 다시 시도하세요.");
                    Console.ReadKey();
                    return this; // 현재 장면 유지
            }
        }
    }
}

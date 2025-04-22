using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Characters;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Scenes
{
    public class CharacterScene : IScene
    {
        public static Character PlayerCharacter { get; private set; } //설정시 이름변경 불가

        public IScene Update()
        {
            Console.Clear();
            Console.WriteLine("--- 캐릭터 생성 ---\n");

            // 이름 입력
            Console.Write("이름을 입력하세요: ");
            string name = Console.ReadLine() ?? "이름없음";



            // 캐릭터 생성
            PlayerCharacter = new Character(name);

            // 생성 완료 메시지
            Console.WriteLine($"\n{name}으로 캐릭터가 생성되었습니다!");
            Console.WriteLine("아무 키나 누르면 계속합니다...");
            Console.ReadKey();

            // 다음 씬으로 이동 (null 반환 시 종료로 간주됨)
            return new MainScene(PlayerCharacter); // 메인씬으로 

        }
    }
}

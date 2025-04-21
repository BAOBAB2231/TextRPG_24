using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Characters;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Scenes
{
    public class StatusScene : IScene
    {
        private readonly Character _player;
        private readonly IScene _previousScene; // 돌아갈 이전 Scene (MainScene)

        public StatusScene(Character player, IScene previousScene)
        {
            _player = player;
            _previousScene = previousScene;
        }

        public IScene Update()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {_player.Level:D2}"); // 레벨 형식 지정 (예: 01)
            Console.WriteLine($"{_player.Name} ( {_player.Job} )");
            // TODO: 아이템 장착 시 공격력 변화 반영
            Console.WriteLine($"공격력 : {_player.Attack}");
            // TODO: 아이템 장착 시 방어력 변화 반영
            Console.WriteLine($"방어력 : {_player.Defense}");
            Console.WriteLine($"체 력 : {_player.CurrentHealth} / {_player.MaxHealth}"); // 현재/최대 체력 표시
            Console.WriteLine($"Gold : {_player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.>> ");

            while (true) // 올바른 입력 받을 때까지 반복
            {
                string input = Console.ReadLine();
                if (input == "0")
                {
                    return _previousScene; // 이전 Scene으로 돌아가기
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 0을 입력하여 나가세요.");
                    Console.Write(">> ");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Characters;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Scenes
{
   
    
        public class MainScene : IScene
        {
            private Character _player; // 플레이어 캐릭터 정보

            // MainScene 생성 시 Character 인스턴스 생성 또는 주입
            // 여기서는 간단하게 새로 생성
            public MainScene()
            {
            //_player = new Character(); ->  updata : 케릭씬에서 만든 케릭으로 캐릭터 생성
            }
        // 다른 Scene에서 돌아올 때 Character 상태를 유지하기 위한 생성자
        public MainScene(Character player)
            {
                _player = player;
            }

            public IScene Update()
            {
                Console.Clear();
                Console.WriteLine("##############################");
                Console.WriteLine("#                            #");
                Console.WriteLine("#         로비 화면          #"); // 화면 이름 변경
                Console.WriteLine("#                            #");
                Console.WriteLine("##############################");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("0. 나가기 (타이틀 화면)"); // 메뉴 변경
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // 상태 보기 Scene으로 전환 (Player 정보 전달)
                        return new StatusScene(_player, this); // 현재 Scene(MainScene)을 전달하여 돌아올 수 있게 함
                    case "2":
                        // 전투 시작 Scene으로 전환 (Player 정보 전달)
                        return new BattleScene(_player, this); // 현재 Scene(MainScene)을 전달하여 돌아올 수 있게 함
                    case "0":
                        // 타이틀 Scene으로 전환
                        return new TitleScene();
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        return this; // 현재 장면 유지
                }
            }
        }
    }


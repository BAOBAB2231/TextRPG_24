using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Interface;

namespace ConsoleApp8.Class
{
    public class GameManager
    {
        private IScene _currentScene;

        public void StartGame(IScene initialScene)
        {
            _currentScene = initialScene;
            GameLoop();
        }

        private void GameLoop()
        {
            while (_currentScene != null)
            {
                // 현재 장면 업데이트 및 다음 장면 받아오기
                _currentScene = _currentScene.Update();
            }
            // 게임 루프 종료 (currentScene이 null이 되면)
            Console.WriteLine("프로그램을 종료합니다.");
        }
    }
}

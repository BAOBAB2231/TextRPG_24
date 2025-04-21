using System;
using ConsoleApp8.Class;
using ConsoleApp8.Scenes;

namespace SpartaDungeon
{
    class Program
    {

        static void Main(string[] args)
        {
            // GameManager 인스턴스 생성
            GameManager gameManager = new GameManager();

            // TitleScene으로 게임 시작
            gameManager.StartGame(new TitleScene());


        }
    }
}

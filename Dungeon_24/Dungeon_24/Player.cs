using System;
namespace SpartaDungeon
{
    public class Player
    {
        public string Name { get; set; } = "용사";
        public string Job { get; set; } = "전사";
        public int Level { get; set; } = 1;
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 500;
        public int MaxHealth { get; set; } = 100;

        public int PreviousHealth { get; set; }
        public void ShowStatus()
        {
            Console.WriteLine("===== 상태 보기 =====");
            Console.WriteLine($"이름 : {Name}");
            Console.WriteLine($"직업 : {Job}");
            Console.WriteLine($"레벨 : {Level}");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체력 : {Health}");
            Console.WriteLine($"골드 : {Gold} G");
            Console.WriteLine("======================");
        }
    }
}
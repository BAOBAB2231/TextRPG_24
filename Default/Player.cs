using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public class Player
    {
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Level { get; set; } = 1;
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public int AttackedDamage { get; set; }
        public bool IsDeadP { get; set; }
        public int AttackedHealth { get; set; }
        public int OriginHP { get; set; }

        public void ShowStatus()
        {
            Console.WriteLine("\n상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}({Job})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체력 : {Health}");
            Console.WriteLine($"Gold : {Gold}G\n");
        }

        public void ShowMe()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{Level}  {Name} [{Job}]");
            Console.WriteLine($"HP {Health}/100\n");
        }

        public int AttackDamage()
        {
            int err = 0;
            err = (int)Math.Ceiling(Attack * 0.1);
            Random random = new Random();
            int damage = random.Next(Attack - err, Attack + err + 1);
            return damage;
        }
    }
}

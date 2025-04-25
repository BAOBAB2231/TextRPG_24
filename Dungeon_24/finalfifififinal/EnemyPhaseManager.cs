using System;
using System.Collections.Generic;

namespace SpartaDungeon
{
    public class EnemyPhaseManager
    {
        public void Execute(Player player, List<Monster> monsters)
        {
            Console.WriteLine("\n===== Enemy Phase =====\n");
            foreach (Monster m in monsters)
            {
                if (m.IsDead) continue;

                Console.WriteLine($"Lv.{m.Level} {m.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.  [데미지 : {m.Attack}]");

                int before = player.Health;
                player.Health -= m.Attack;
                if (player.Health < 0) player.Health = 0;

                Console.WriteLine($"Lv.{player.Level} {player.Name}\nHP {before} -> {player.Health}\n");
                Console.WriteLine("0. 다음");
                Console.ReadLine();
            }
        }
    }
}

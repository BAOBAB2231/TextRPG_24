using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_24_J
{
    public class LevelUp
    {
        int[] expToLevelUp = { 10, 35, 65, 100 };
        public void AddExp(Player player, List<Monster> monsters)
        {
            int expToAdd = 0;
            int bExp = player.Exp;
            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                expToAdd += monster.Exp;
            }
            player.Exp += expToAdd;
            int needExp = expToLevelUp[player.Level - 1];
            if (player.Exp > needExp)
            {
                player.Exp -= needExp;
                player.Level++;
                player.BaseAttack += 1;
                player.BaseDefense += 1;
                Console.WriteLine($"Lv.{player.Level - 1} {player.Name} -> Lv.{player.Level} {player.Name}");
                Console.WriteLine($"exp {bExp} -> {player.Exp}");
            }
            else
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"exp {bExp} -> {player.Exp}");
            }
        }
    }
}

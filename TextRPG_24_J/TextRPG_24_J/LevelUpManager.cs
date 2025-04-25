
using System;

namespace TextRPG_24_J
{
    public static class LevelUpManager
    {
        // 플레이어의 경험치를 증가시키고, 필요 시 레벨업 처리
        public static void AddExp(Player player, int expGained)
        {
            player.Exp += expGained;

            while (player.Exp >= player.MaxExp)
            {
                player.Exp -= player.MaxExp;
                player.Level++;
                player.BaseAttack += 2;
                player.MaxHp += 5; // 최대 체력만 증가
                player.MaxExp = 40 * player.Level;

                Console.WriteLine("\n[레벨 업!]");
                Console.WriteLine($"레벨이 {player.Level}로 올랐습니다!");
                Console.WriteLine($"공격력이 2 증가하여 {player.Attack}이 되었습니다.");
                Console.WriteLine($"최대 체력이 5 증가하여 {player.MaxHp}이 되었습니다.\n");
            }
        }
    }
}

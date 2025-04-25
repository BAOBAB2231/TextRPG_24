using System;

namespace SpartaDungeon
{
    public class ExpSet
    {
        private int totalGoldReward = 0;
        private int totalExpReward = 0;

        public void GainRewards(Player player, Monster monster)
        {
            int expAmount = monster.ExpReward;
            int goldAmount = monster.GoldReward;

            player.Exp += expAmount;
            player.Gold += goldAmount;
            totalExpReward += expAmount;
            totalGoldReward += goldAmount;

            Console.WriteLine($"[EXP +{expAmount}] 경험치를 획득했습니다! (누적 경험치: {player.Exp} / 필요 경험치: {player.MaxExp})");
            Console.WriteLine($"[GOLD +{goldAmount}G] 골드를 획득했습니다!");

            CheckLevelUp(player);
        }

        private void CheckLevelUp(Player player)
        {
            while (player.Exp >= player.MaxExp)
            {
                int beforeAtk = player.Attack;
                int beforeHp = player.MaxHealth;

                player.Exp -= player.MaxExp;
                player.Level++;
                player.MaxExp = LevelExpCalculator.GetNextLevelExp(player.Level);

                player.Attack += 2;
                player.MaxHealth += 5;
                player.Health = player.MaxHealth;

                Console.WriteLine("\n===== 레벨 업! =====");
                Console.WriteLine($"레벨이 {player.Level}이(가) 되었습니다!");
                Console.WriteLine($"공격력 +2 → 현재 공격력: {player.Attack}");
                Console.WriteLine($"체력 +5 → 최대 체력: {player.MaxHealth}");
                Console.WriteLine("====================\n");
            }
        }

        public void PrintTotalRewards()
        {
            Console.WriteLine("[전투 요약]");
            Console.WriteLine($"- 획득 경험치: {totalExpReward}");
            Console.WriteLine($"- 획득 골드: {totalGoldReward} G\n");

            totalExpReward = 0;
            totalGoldReward = 0;
        }

        public void ApplyPenalty()
        {
            totalExpReward = (int)(totalExpReward * 0.8);
            totalGoldReward = (int)(totalGoldReward * 0.8);
        }
    }
}

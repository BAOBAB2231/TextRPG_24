using System;
using System.Collections.Generic;
using System.Linq;

namespace SpartaDungeon
{
    public class BattleResultChecker
    {
        // 플레이어의 체력이 0 이하이면 패배로 판단
        public bool IsPlayerDefeated(Player player)
        {
            return player.Health <= 0;
        }

        // 모든 몬스터가 사망했는지 확인 (승리 조건)
        public bool AreAllMonstersDefeated(List<Monster> monsters)
        {
            return monsters.All(m => m.IsDead);
        }
    }
}

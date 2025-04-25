// 미니언 몬스터 클래스 (초보자 기준 주석 포함)
using System;

namespace SpartaDungeon
{
    public class Minion : Monster
    {
        public override void InitStats()
        {
            Name = "미니언";
            Level = 1;
            Health = 15;
            Attack = 5;
            ExpReward = 10;
            GoldReward = 300;
        }
    }
}

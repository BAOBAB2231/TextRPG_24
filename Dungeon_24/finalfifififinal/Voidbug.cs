using System;

namespace SpartaDungeon
{
    public class Voidbug : Monster
    {
        public override void InitStats()
        {
            Name = "공허충";
            Level = 3;
            Health = 30;
            Attack = 8;
            ExpReward = 20;
            GoldReward = 500;
        }
    }
}

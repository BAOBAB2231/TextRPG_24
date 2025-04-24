using System;

namespace SpartaDungeon
{
    public class CannonMinion : Monster
    {
        public override void InitStats()
        {
            Name = "대포미니언";
            Level = 5;
            Health = 50;
            Attack = 12;
            ExpReward = 25;
            GoldReward = 700;
        }
    }
}

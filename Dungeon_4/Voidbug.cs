namespace SpartaDungeon
{
    public class Voidbug : Monster
    {
        public override void InitStats()
        {
            Name = "공허벌레";      // 이름
            Level = 2;             // 중간 난이도
            MaxHealth = 20;        // 체력이 낮음
            Health = MaxHealth;    // 현재 체력 초기화
            Attack = 7;            // 공격력은 비교적 높음
        }
    }
}
namespace SpartaDungeon
{
    public class CannonMinion : Monster
    {
        public override void InitStats()
        {
            Name = "대포 미니언";    // 이름
            Level = 5;               // 고레벨 설정
            MaxHealth = 50;          // 체력이 매우 높음
            Health = MaxHealth;      // 체력 초기화
            Attack = 10;             // 높은 공격력
        }
    }
}
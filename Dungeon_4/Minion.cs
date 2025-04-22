namespace SpartaDungeon
{
    public class Minion : Monster
    {
        public override void InitStats()
        {
            Name = "미니언";      // 몬스터 이름
            Level = 1;           // 기본 레벨
            MaxHealth = 30;      // 체력
            Health = MaxHealth; // 현재 체력은 최대치로 시작
            Attack = 5;          // 공격력
        }
    }
}
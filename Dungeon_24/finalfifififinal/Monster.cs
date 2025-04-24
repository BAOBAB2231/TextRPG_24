namespace SpartaDungeon
{
    public class Monster
    {
        public string Name { get; set; }         // 이름
        public int Level { get; set; }           // 레벨
        public int Health { get; set; }          // 현재 체력
        public int Attack { get; set; }          // 공격력
        public int ExpReward { get; set; }       // 처치 시 획득 경험치
        public int GoldReward { get; set; }      // 처치 시 획득 골드
        public bool IsDead { get; set; } = false; // 죽었는지 여부

        // 자식 클래스에서 스탯을 정의하도록 가상 메서드 제공
        public virtual void InitStats() { }
    }
}

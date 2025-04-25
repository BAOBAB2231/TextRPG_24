namespace SpartaDungeon
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public bool IsDead { get; set; }
        public virtual void InitStats() { }
    }
}
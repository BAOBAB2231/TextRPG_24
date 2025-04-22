namespace SpartaDungeon
{
    public class Voidbug : Monster
    {
        public override void InitStats()
        {
            Name = "공허충";      
            Level = 2;            
            MaxHealth = 20;        
            Health = MaxHealth;   
            Attack = 7;            
        }
    }
}
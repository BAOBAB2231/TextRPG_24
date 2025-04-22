namespace SpartaDungeon
{
    public class Minion : Monster
    {
        public override void InitStats()
        {
            Name = "미니언";     
            Level = 1;          
            MaxHealth = 30;      
            Health = MaxHealth; 
            Attack = 5;         
        }
    }
}
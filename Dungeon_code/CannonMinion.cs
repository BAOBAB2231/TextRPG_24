namespace SpartaDungeon
{
    public class CannonMinion : Monster
    {
        public override void InitStats()
        {
            Name = "대포 미니언";    
            Level = 5;              
            MaxHealth = 50;          
            Health = MaxHealth;      
            Attack = 10;           
        }
    }
}
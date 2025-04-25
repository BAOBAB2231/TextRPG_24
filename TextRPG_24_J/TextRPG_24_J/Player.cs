

namespace TextRPG_24_J
{
    public class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }

        public List<Item> EquippedItems { get; } = new List<Item>();
        public void Equip(Item item)
        {
            if (!item.IsEquipped)
            {
                EquippedItems.Add(item);
                item.IsEquipped = true;
            }
        }

        public void Unequip(Item item)
        {
            if (item.IsEquipped)
            {
                EquippedItems.Remove(item);
                item.IsEquipped = false;
            }
        }
        public int Attack
        {
            get
            {
                int value = BaseAttack;
                foreach (var item in EquippedItems)
                {
                    if (item.StatType == "공격력")
                        value += item.StatValue;
                }
                return value;
            }
        }

        public int Defense
        {
            get
            {
                int value = BaseDefense;
                foreach (var item in EquippedItems)
                {
                    if (item.StatType == "방어력")
                        value += item.StatValue;
                }
                return value;
            }
        }

        public Player(string name, string job, int level, int attack, int defense, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            BaseAttack = attack;
            BaseDefense = defense;
            HP = hp;
            Gold = gold;
        }
        public void ShowStatus()
        {
            Console.WriteLine("\n상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체  력 : {HP}");
            Console.WriteLine($"Gold : {Gold} G\n");
            
        }

    }
}

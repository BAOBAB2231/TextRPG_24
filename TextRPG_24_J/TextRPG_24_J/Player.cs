

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
        public int MaxMana { get; set; }  // 최대 마나

        public int CurrentMana { get; set; } // 현재 마나

        public int Gold { get; set; }

        public List<Skills> SkillList { get; set; }

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

        public Player(string name, string job, int level, int attack, int defense, int hp, int gold )
        {
            Name = name;
            Job = job;
            Level = level;
            BaseAttack = attack;
            BaseDefense = defense;
            HP = hp;
            Gold = gold;
            MaxMana = 50;
            CurrentMana = MaxMana;
            SkillList = new List<Skills>();
            GetSkill();
        }

        void GetSkill()
        {
            SkillList.Add(new Skills(
                name: "알파 스트라이크",
                mpCost: 5,
                description: "공격력 X 2 로 하나의 적을 공격합니다.\n",
                damageMultiplier: 2,
                SkillType.SingleTarget)
                );
            SkillList.Add(new Skills(
                name: "더블 스트라이크",
                mpCost: 15,
                description: "공격력 X 1.5 로 두명의 적을 공격합니다.\n",
                damageMultiplier: 1.5f,
                 SkillType.RandomTarget,
                numberofTargets: 2));
            SkillList.Add(new Skills(
                name: "명상",
                mpCost: 10,
                description: "명상에 들어가서 공격력X2 만큼 회복합니다.\n", 
                damageMultiplier: 0,
                SkillType.SelfTarget,
                numberofTargets: 0,
                healAmount: Attack * 2
                ));

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
            Console.WriteLine($"마  나 : {CurrentMana}");
            Console.WriteLine($"Gold : {Gold} G\n");
            
        }

    }
}

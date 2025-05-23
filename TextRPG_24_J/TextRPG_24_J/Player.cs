﻿namespace TextRPG_24_J
{
    public class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int HP { get; set; }
        public int MaxHp { get; set; }           // ✅ 추가된 속성
        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }

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

        public Player(string name, string job, int level, int attack, int defense, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            BaseAttack = attack;
            BaseDefense = defense;
            HP = hp;
            MaxHp = hp; // ✅ 생성자에서 초기화
            Gold = gold;
            MaxMana = 50;
            CurrentMana = MaxMana;
            Exp = 0;
            MaxExp = 40;
            SkillList = new List<Skills>();
            GetSkill();
        }

        void GetSkill()
        {
            SkillList = Skills.GetSkill(Attack);
        }

        public void ShowStatus()
        {
            Console.WriteLine("\n상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체  력 : {HP} / {MaxHp}"); // ✅ MaxHp 표시
            Console.WriteLine($"마  나 : {CurrentMana}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine($"Exp  : {Exp} / {MaxExp}\n");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Scripts.Characters;

namespace ConsoleApp8.Characters
{
    public class Character
    {
        public int Level { get; set; }
        public string Name { get; } // 이름은 생성 시 설정 후 변경 불가
        public string Job { get; } // 직업도 생성 시 설정 후 변경 불가
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MaxHealth { get; set; } // 최대 체력
        public int CurrentHealth { get; set; } // 현재 체력       
        public int MaxMana { get; set; }  // 최대 마나

        public int CurrentMana { get; set; } // 현재 마나
        public int Gold { get; set; }

        public List<Skills> SkillList { get; set; }


        // 기본 생성자: 초기값 설정
        public Character(string name = "", string job = "전사")
        {
            Level = 1;
            Name = name;
            Job = job;
            Attack = 10;
            Defense = 5;
            MaxHealth = 100;
            CurrentHealth = MaxHealth; // 시작 시 체력은 최대로
            MaxMana = 50;
            CurrentMana = MaxMana;
            Gold = 1500;
            SkillList = new List<Skills>();
            GetSkill();
        }

     


        void GetSkill()
        {
            SkillList.Add(new Skills(
                name:"알파 스트라이크",
                mpCost: 5,
                description: "공격력 X 2 로 하나의 적을 공격합니다.",
                damageMultiplier: 2,
                SkillType.SingleTarget)
                );
            SkillList.Add(new Skills(
                name: "더블 스트라이크",
                mpCost: 15,
                description: "공격력 X 1.5 로 두명의 적을 공격합니다.",
                damageMultiplier: 1.5f,
                 SkillType.RandomTarget,
                numberofTargets: 1));
            SkillList.Add(new Skills(
                name: "명상",
                mpCost: 10, 
                description: "명상에 들어가서 회복합니다.", 
                damageMultiplier: 0, 
                SkillType.SelfTarget));

            }



            // 캐릭터가 살아있는지 확인하는 메서드
        public bool IsAlive => CurrentHealth > 0;

        // 피격 처리 메서드
        public void TakeDamage(int damage)
        {
            int finalDamage = Math.Max(0, damage - Defense); // 방어력 고려, 최소 데미지는 0
            CurrentHealth -= finalDamage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            Console.WriteLine($"{Name}이(가) {finalDamage}의 데미지를 받았습니다."); // 피격 로그 추가
            Console.WriteLine($"현재 체력: {CurrentHealth}/{MaxHealth}");
        }
    }
}

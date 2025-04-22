using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Gold { get; set; }

        // 기본 생성자: 초기값 설정
        public Character(string name = "Chad", string job = "전사")
        {
            Level = 1;
            Name = name;
            Job = job;
            Attack = 10;
            Defense = 5;
            MaxHealth = 100;
            CurrentHealth = MaxHealth; // 시작 시 체력은 최대로
            Gold = 1500;
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

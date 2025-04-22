using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp8.Characters;

namespace ConsoleApp8.Monsters
{
    public abstract class Monster // 상속을 위한 추상 클래스
    {
        public int Level { get; protected set; }
        public string Name { get; protected set; }
        public int MaxHealth { get; protected set; } // Readonly 대신 protected set으로 변경
        public int CurrentHealth { get; set; }
        public int Attack { get; protected set; }
        public bool IsDead => CurrentHealth <= 0;

        protected Monster(int level, string name, int maxHealth, int attack)
        {
            Level = level;
            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Attack = attack;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }

        // 몬스터 공격 (플레이어 캐릭터 대상)
        public void AttackPlayer(Character player)
        {
            if (IsDead || !player.IsAlive) return; // 죽었거나 플레이어가 죽었으면 공격 안 함

            Console.WriteLine($"Lv.{Level} {Name} 의 공격!");
            player.TakeDamage(Attack);
        }
    }

    public class Minion : Monster
    {
        public Minion() : base(2, "미니언", 15, 5) { }
    }

    public class CannonMinion : Monster
    {
        public CannonMinion() : base(5, "대포미니언", 25, 8) { } // 공격력 추가
    }

    public class VoidBug : Monster
    {
        public VoidBug() : base(3, "공허충", 10, 3) { } // 공격력 추가
    }
}

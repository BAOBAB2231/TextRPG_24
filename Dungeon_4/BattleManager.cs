using System;
using System.Collections.Generic;
namespace SpartaDungeon
{
    public class BattleManager
    {
        private Player player;
        private Random random = new Random();
        private List<Monster> monsterList = new List<Monster>();
        public BattleManager(Player player)
        {
            this.player = player;
        }
        public void StartBattle()
        {
            monsterList = GenerateRandomMonsters();
            player.PreviousHealth = player.Health;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Battle Start! =====\n");
                for (int i = 0; i < monsterList.Count; i++)
                {
                    Monster m = monsterList[i];
                    string status = m.IsDead ? "Dead" : $"HP: {m.Health}/{m.MaxHealth}";
                    Console.WriteLine($"{i + 1}. {m.Name} (Lv.{m.Level}) - {status}");
                }
                Console.WriteLine("\n0. 전투 종료");
                Console.Write("공격할 몬스터 번호를 입력하세요: ");
                string input = Console.ReadLine();
                int choice;
                if (!int.TryParse(input, out choice) || choice < 0 || choice > monsterList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르세요.");
                    Console.ReadKey();
                    continue;
                }
                if (choice == 0)
                    break;
                Monster target = monsterList[choice - 1];
                if (target.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                }
                else
                {
                    int damage = player.Attack;
                    target.Health -= damage;
                    Console.WriteLine($"{player.Name}이(가) {target.Name}에게 {damage}의 데미지를 입혔습니다!");
                    if (target.Health <= 0)
                    {
                        target.Health = 0;
                        target.IsDead = true;
                        player.Gold += 100;
                        Console.WriteLine($"{target.Name} 처치! 보상으로 100골드 획득!");
                    }
                    int counter = target.Attack - player.Defense;
                    if (counter > 0)
                    {
                        player.Health -= counter;
                        Console.WriteLine($"{target.Name}의 반격! {counter} 데미지!");
                    }
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("플레이어가 사망했습니다...");
                        break;
                    }
                }
                Console.WriteLine("\nEnter 키를 누르면 계속합니다...");
                Console.ReadLine();
            }
        }
        private List<Monster> GenerateRandomMonsters()
        {
            List<Monster> list = new List<Monster>();
            for (int i = 0; i < 3; i++)
            {
                int r = random.Next(0, 3);
                Monster m;
                if (r == 0) m = new Minion();
                else if (r == 1) m = new Voidbug();
                else m = new CannonMinion();
                m.Level = random.Next(1, 5);
                m.InitStats();
                m.Health = m.MaxHealth;
                m.IsDead = false;
                list.Add(m);
            }
            return list;
        }
    }
}
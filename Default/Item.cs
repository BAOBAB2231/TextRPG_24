using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public enum ItemType
    {
        Armor = 0, // 방어구
        Weapon = 1, // 무기
        Accessory = 2 // 장신구
    }
    public class Item
    {
        public string ItemName { get; set; }              //프로퍼티 아이템 이름
        public string ItemDescription { get; set; }       //아이템 설명
        public int ItemAttack { get; set; }           //아이템 공격력
        public int ItemDefense { get; set; }          //아이템 방어력
        public int ItemPrice { get; set; }            //아이템 가격
        public bool IsPlayerItem { get; set; } = false; //아이템이 플레이어의 아이템인지 확인하는 변수
        public bool IsEquipped { get; set; } = false; //아이템이 장착된 상태인지 확인하는 변수

        public ItemType TypeOfItem { get; set; }               //아이템 타입 (0 방어구, 1 무기, 2 장신구)
    }

    public class ItemFactory
    {
        public List<Item> Items = new List<Item>();

        public ItemFactory() 
        {
            MakeItems();
        }

        public void MakeItems()
        {
            Items.Add(new Item
            {
                ItemName = "수련자 갑옷",
                ItemDescription = "수련에 도움을 주는 갑옷입니다.",
                ItemAttack = 0,
                ItemDefense = 5,
                ItemPrice = 1000,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Armor
            });

            Items.Add(new Item
            {
                ItemName = "무쇠갑옷",
                ItemDescription = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                ItemAttack = 0,
                ItemDefense = 9,
                ItemPrice = 2000,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Armor
            });

            Items.Add(new Item
            {
                ItemName = "스파르타의 갑옷",
                ItemDescription = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                ItemAttack = 0,
                ItemDefense = 15,
                ItemPrice = 3500,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Armor
            });

            Items.Add(new Item
            {
                ItemName = "낡은 검",
                ItemDescription = "쉽게 볼 수 있는 낡은 검 입니다.",
                ItemAttack = 2,
                ItemDefense = 0,
                ItemPrice = 600,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Weapon
            });

            Items.Add(new Item
            {
                ItemName = "청동 도끼",
                ItemDescription = "어디선가 사용됐던거 같은 도끼입니다.",
                ItemAttack = 5,
                ItemDefense = 0,
                ItemPrice = 1500,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Weapon
            });

            Items.Add(new Item
            {
                ItemName = "스파르타의 창",
                ItemDescription = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                ItemAttack = 15,
                ItemDefense = 0,
                ItemPrice = 4000,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Weapon
            });

            Items.Add(new Item
            {
                ItemName = "낡은 키보드와 마우스",
                ItemDescription = "코딩근육을 키워봅시다.",
                ItemAttack = 4,
                ItemDefense = 3,
                ItemPrice = 1500,
                IsPlayerItem = false,
                TypeOfItem = ItemType.Accessory
            });
        }
    }
}
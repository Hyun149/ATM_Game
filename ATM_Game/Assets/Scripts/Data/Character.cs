using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 데이터를 담는 클래스입니다.
/// </summary>
[System.Serializable]
public class Character
{
    public string characterName;
    public int level;
    public int gold;
    public int attack;
    public int defense;
    public int hp;

    public List<Item> Inventory { get; private set; } = new List<Item>();

    public Character(UserData user)
    {
        this.characterName = user.userName;
        this.level = Mathf.Max(1, user.balance / 37145); //최소 1레벨 보장
        this.gold = user.balance / 34;
        CalculateStats();
    }

    /// <summary>
    /// 레벨을 기반으로 스탯을 계산합니다.
    /// </summary>
    private void CalculateStats()
    {
        this.attack = level * 3;
        this.defense = level * 3;
        this.hp = level * 11;

        foreach (var item in Inventory)
        {
            if (item.isEquipped)
            {
                attack += item.data.attackBonus;
                defense += item.data.defenseBonus;
                hp += item.data.hpBonus;
            }
        }
    }

    public void AddItem(ItemData itemData)
    {
        Inventory.Add(new Item(itemData));
        RecalculateStats();
    }

    public void EquipItem(Item item)
    {
        if (item.isEquipped)
        {
            return;
        }

        if (Inventory.Contains(item))
        {
            item.Equip();
            RecalculateStats();
        }

    }

    public void UnEquipItem(Item item)
    {
        if (Inventory.Contains(item))
        {
            item.UnEquip();
            RecalculateStats();
        }
    }

    private void RecalculateStats()
    {
        CalculateStats();
    }
}


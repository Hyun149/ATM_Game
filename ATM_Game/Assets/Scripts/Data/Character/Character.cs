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

    public CharacterStats Stats { get; private set; }
    public CharacterInventory Inventory { get; private set; }

    public Character(UserData user)
    {
        this.characterName = user.userName;
        this.level = Mathf.Max(1, user.balance / 37145); //최소 1레벨 보장
        this.gold = user.balance / 34;

        Inventory = new CharacterInventory();

        foreach (var itemSave in user.inventory)
        {
            Debug.Log($"[불러오기]: {itemSave.itemid} / 장착: {itemSave.isEquipped}");

            var itemData = Resources.Load<ItemData>($"ItemData/{itemSave.itemid}");
            if (itemData != null)
            {
                Debug.Log($"아이템 로드 성공: {itemData.name}");
                var item = Inventory.AddItem(itemData);
                if (itemSave.isEquipped)
                {
                    Inventory.Equip(item);
                }
            }
            else
            {
                Debug.LogError($"아이템 로드 실패: {itemSave.itemid}");
            }
        }

        Stats = new CharacterStats(level, Inventory);
    }

    public void AddItem(ItemData data)
    {
        Inventory.AddItem(data);
        Stats.ReCalualate();
    }

    public void Equip(Item item)
    {
        Inventory.Equip(item);
        Stats.ReCalualate();
    }

    public void UnEquip(Item item)
    {
        Inventory.UnEquip(item);
        Stats.ReCalualate();
    }
}


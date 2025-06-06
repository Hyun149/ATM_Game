using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 데이터를 담는 클래스입니다.<br/>
/// - 유저 데이터를 기반으로 초기화되며, 레벨과 골드를 계산하여 설정합니다.<br/>
/// - 캐릭터의 인벤토리 및 능력치(Stats)를 관리하며, 아이템 장착/해제에 따라 스탯을 자동 갱신합니다.
/// </summary>
[System.Serializable]
public class Character
{
    public string characterName;
    public int level;
    public int gold;

    /// <summary>
    /// 캐릭터의 현재 능력치를 관리하는 객체입니다.
    /// </summary>
    public CharacterStats Stats { get; private set; }

    /// <summary>
    /// 캐릭터가 보유한 인벤토리 정보입니다.
    /// </summary>
    public CharacterInventory Inventory { get; private set; }

    /// <summary>
    /// 유저 데이터를 기반으로 캐릭터를 초기화합니다.<br/>
    /// - 레벨은 잔액 기반으로 설정되며 최소 1레벨을 보장합니다.<br/>
    /// - 초기 골드도 잔액에서 계산됩니다.<br/>
    /// - 저장된 인벤토리 정보를 로드하여 아이템을 복원하고 장착 처리까지 수행합니다.<br/>
    /// - 능력치는 레벨과 인벤토리 정보를 바탕으로 초기화됩니다.
    /// </summary>
    /// <param name="user">불러올 유저 데이터</param>
    public Character(UserData user)
    {
        this.characterName = user.userName;
        this.level = Mathf.Max(1, user.balance / 10000); //최소 1레벨 보장
        this.gold = user.balance / 10;

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

    /// <summary>
    /// 새로운 아이템을 인벤토리에 추가하고 능력치를 갱신합니다.
    /// </summary>
    /// <param name="data">추가할 아이템 데이터</param>
    public void AddItem(ItemData data)
    {
        Inventory.AddItem(data);
        Stats.ReCalculate();
    }

    /// <summary>
    /// 특정 아이템을 장착하고 능력치를 다시 계산합니다.
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    public void Equip(Item item)
    {
        Inventory.Equip(item);
        Stats.ReCalculate();
    }

    /// <summary>
    /// 특정 아이템을 해제하고 능력치를 다시 계산합니다.
    /// </summary>
    /// <param name="item">해제할 아이템</param>
    public void UnEquip(Item item)
    {
        Inventory.UnEquip(item);
        Stats.ReCalculate();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 런타임에 사용되는 인벤토리 아이템 클래스입니다.
/// </summary>
public class Item
{
    public ItemData data {  get; private set; }
    public bool isEquipped { get; private set; }

    public Item(ItemData data)
    {
        this.data = data;
        this.isEquipped = false;
    }

    public void Equip() => isEquipped = true;

    public void UnEquip() => isEquipped = false;
}

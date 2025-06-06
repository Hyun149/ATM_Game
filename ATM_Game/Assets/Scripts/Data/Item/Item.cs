using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 런타임에 사용되는 인벤토리 아이템 클래스입니다.<br/>
/// - ItemData를 참조하며, 장착 여부를 포함한 아이템 상태를 관리합니다.
/// </summary>
public class Item
{
    /// <summary>
    /// 아이템의 데이터 정보입니다.<br/>
    /// - 이름, 스탯 보너스 등의 메타 정보를 포함합니다.
    /// </summary>
    public ItemData data {  get; private set; }

    /// <summary>
    /// 아이템이 장착되어 있는지 여부를 나타냅니다.
    /// </summary>
    public bool isEquipped { get; private set; }

    /// <summary>
    /// 아이템을 생성합니다.<br/>
    /// - 주어진 ItemData를 바탕으로 아이템 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="data">참조할 아이템 데이터</param>
    public Item(ItemData data)
    {
        this.data = data;
        this.isEquipped = false;
    }

    /// <summary>
    /// 아이템을 장착 상태로 변경합니다.
    /// </summary>
    public void Equip() => isEquipped = true;

    /// <summary>
    /// 아이템을 장착 해제 상태로 변경합니다.
    /// </summary>
    public void UnEquip() => isEquipped = false;
}

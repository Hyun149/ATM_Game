using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 캐릭터의 인벤토리를 관리하는 클래스입니다.
/// 아이템 추가, 장착/해제, 장착 중인 아이템 조회 등의 기능을 제공합니다.
/// </summary>
public class CharacterInventory
{
    private List<Item> items = new();

    /// <summary>
    /// 인벤토리에 보유 중인 아이템 목록을 가져옵니다.
    /// </summary>
    public List<Item> Items => items;

    /// <summary>
    /// 새로운 아이템을 인벤토리에 추가합니다.
    /// </summary>
    /// <param name="data">아이템 데이터</param>
    /// <returns>생성된 인벤토리 아이템</returns>
    public Item AddItem(ItemData data)
    {
        var item = new Item(data);
        items.Add(item);
        return item;
    }

    /// <summary>
    /// 지정한 아이템을 장착합니다.
    /// 이미 장착 중이거나 인벤토리에 없는 경우 무시됩니다.
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    public void Equip(Item item)
    {
        if (item.isEquipped || !items.Contains(item)) 
        {
            return;
        }
        item.Equip();
    }

    /// <summary>
    /// 지정한 아이템의 장착을 해제합니다.
    /// 인벤토리에 없는 경우 무시됩니다.
    /// </summary>
    public void UnEquip(Item item)
    {
        if (!items.Contains(item))
        {
            return;
        }
        item.UnEquip();
    }

    /// <summary>
    /// 현재 장착 중인 모든 아이템을 반환합니다.
    /// </summary>
    /// <returns>장착된 아이템 컬렉션</returns>
    public IEnumerable<Item> GetEquippedItems()
    {
        return items.Where(i => i.isEquipped);
    }
}

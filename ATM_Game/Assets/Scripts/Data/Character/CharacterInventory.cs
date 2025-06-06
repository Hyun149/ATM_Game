using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory
{
    private List<Item> items = new();

    public List<Item> Items => items;

    public void AddItem(ItemData data)
    {
        items.Add(new Item(data));
    }

    public void Equip(Item item)
    {
        if (item.isEquipped || !items.Contains(item)) 
        {
            return;
        }
        item.Equip();
    }

    public void UnEquip(Item item)
    {
        if (!items.Contains(item))
        {
            return;
        }
        item.UnEquip();
    }

    public IEnumerable<Item> GetEquippedItems()
    {
        return items.Where(i => i.isEquipped);
    }
}

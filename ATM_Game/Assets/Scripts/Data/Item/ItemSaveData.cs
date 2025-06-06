using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSaveData
{
    public string itemid;
    public bool isEquipped;

    public ItemSaveData(string itemid, bool isEquipped)
    {
        this.itemid = itemid;
        this.isEquipped = isEquipped;
    }
}

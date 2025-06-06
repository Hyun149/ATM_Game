using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int HP { get; private set; }

    private int baseLevel;
    private CharacterInventory inventory;

    public CharacterStats(int level, CharacterInventory inventory)
    {
        baseLevel = level;
        this.inventory = inventory;
        ReCalualate();
    }

    public void ReCalualate()
    {
        Attack = baseLevel * 3;
        Defense = baseLevel * 2;
        HP = baseLevel * 11;

        foreach (var item in inventory.GetEquippedItems())
        {
            Attack += item.data.attackBonus;
            Defense += item.data.defenseBonus;
            HP += item.data.hpBonus;
        }
    }
}

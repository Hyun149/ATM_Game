using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 능력치를 계산하고 보관하는 클래스입니다.<br/>
/// - 레벨과 장착 아이템을 기반으로 공격력, 방어력, 체력을 계산합니다.<br/>
/// - 장착 아이템 변경 시 ReCalculate()를 호출하여 능력치를 갱신해야 합니다.
/// </summary>
public class CharacterStats
{
    /// <summary>최종 계산된 공격력</summary>
    public int Attack { get; private set; }

    /// <summary>최종 계산된 방어력</summary>
    public int Defense { get; private set; }

    /// <summary>최종 계산된 체력</summary>
    public int HP { get; private set; }

    private int baseLevel;
    private CharacterInventory inventory;

    /// <summary>
    /// 레벨과 인벤토리를 기반으로 스탯 시스템을 초기화합니다.
    /// </summary>
    /// <param name="level">기준 레벨</param>
    /// <param name="inventory">장착 아이템 정보를 보유한 인벤토리</param>
    public CharacterStats(int level, CharacterInventory inventory)
    {
        baseLevel = level;
        this.inventory = inventory;
        ReCalculate();
    }

    /// <summary>
    /// 현재 레벨과 장착 아이템을 기반으로 능력치를 다시 계산합니다.<br/>
    /// - 레벨에 비례하는 기본 수치 + 장착 아이템의 보너스를 합산합니다.
    /// </summary>
    public void ReCalculate()
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

using UnityEngine;

[CreateAssetMenu(fileName = " NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int attackBonus;
    public int defenseBonus;
    public int hpBonus;
}

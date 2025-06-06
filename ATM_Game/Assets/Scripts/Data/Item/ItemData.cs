using UnityEngine;

/// <summary>
/// 인벤토리 아이템의 데이터 정보를 담는 ScriptableObject입니다.<br/>
/// - 이름, 아이콘, 각종 능력치 보너스를 포함하며, 에디터에서 생성 가능합니다.
/// </summary>
[CreateAssetMenu(fileName = " NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int attackBonus;
    public int defenseBonus;
    public int hpBonus;
}

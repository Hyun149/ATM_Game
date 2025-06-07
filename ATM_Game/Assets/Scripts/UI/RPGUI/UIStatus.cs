using UnityEngine;
using TMPro;

/// <summary>
/// <b>캐릭터의 능력치를 UI에 표시하는 클래스입니다.</b><br/>
/// - 기본 능력치(공격력, 방어력, 체력)를 표시합니다.<br/>
/// - 장착 아이템에 의한 추가 능력치도 별도로 계산하여 표시합니다.
/// </summary>
public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI EquipStatusText;

    /// <summary>
    /// UI가 활성화될 때 호출되며, 캐릭터의 현재 능력치를 UI에 표시합니다.
    /// </summary>
    private void OnEnable()
    {
        var character = GameManager.Instance.PlayerCharacter;

        if (character != null)
        {
            statusText.text =
                $"공격력: {character.Stats.Attack}\n" +
                $"방어력: {character.Stats.Defense}\n" +
                $"체력: {character.Stats.HP}";

            Refresh();
        }
        else
        {
            statusText.text = "캐릭터 정보가 없습니다.";
            Debug.LogWarning("UIStatus: 캐릭터 정보가 null입니다.");
        }
    }

    /// <summary>
    /// 장착된 아이템을 기준으로 추가 능력치를 계산하여 UI에 표시합니다.
    /// </summary>
    public void Refresh()
    {
        var character = GameManager.Instance.PlayerCharacter;

        if (character != null)
        {
            int equipAtk = 0;
            int equipDef = 0;
            int equipHP = 0;

            foreach (var item in character.Inventory.Items)
            {
                if (item.isEquipped)
                {
                    equipAtk += item.data.attackBonus;
                    equipDef += item.data.defenseBonus;
                    equipHP += item.data.hpBonus;
                }
            }

            EquipStatusText.text =
                $"+ {equipAtk} 공격력\n" +
                $"+ {equipDef} 방어력\n" +
                $"+ {equipHP}  체력";

        }
    }
}

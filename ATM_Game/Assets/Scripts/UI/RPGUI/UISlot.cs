using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// <b>인벤토리의 한 칸(슬롯)을 관리하는 UI 컴포넌트입니다.</b><br/>
/// - 아이템의 아이콘을 표시하고, 장착 여부에 따라 시각 효과를 보여줍니다.<br/>
/// - 클릭 시 아이템 장착/해제를 처리합니다.
public class UISlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject equipEffect;

    private Item currentItem;

    /// <summary>
    /// 슬롯에 아이템을 설정하고 아이콘을 표시합니다.
    /// </summary>
    /// <param name="item">UI에 표시할 아이템</param>
    public void SetItem(Item item)
    {
        currentItem = item;
        iconImage.sprite = item.data.icon;
        iconImage.enabled = iconImage.sprite != null;

        RefreshUI();
    }

    /// <summary>
    /// 슬롯 클릭 시 호출되는 메서드입니다.<br/>
    /// - 아이템이 장착되어 있다면 해제하고, 그렇지 않다면 장착합니다.<br/>
    /// - 장착 상태에 따라 UI를 갱신합니다.
    /// </summary>
    public void OnClick()
    {
        if (currentItem == null) return;

        var character = GameManager.Instance.PlayerCharacter;

        if (currentItem.isEquipped)
        {
            character.UnEquip(currentItem);
        }
        else
        {
            character.Equip(currentItem);
        }

        RefreshUI();
    }

    /// <summary>
    /// 장착 상태에 따라 슬롯의 시각적 효과를 갱신합니다.<br/>
    /// - 장착된 경우 효과를 활성화하고, 아니라면 비활성화합니다.
    /// </summary>
    public void RefreshUI()
    {
        if (equipEffect != null)
        {
            equipEffect.SetActive(currentItem != null && currentItem.isEquipped);
        }
    }
}

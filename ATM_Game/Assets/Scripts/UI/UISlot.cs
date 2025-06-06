using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리의 한 칸(슬롯)을 관리하는 UI 컴포넌트입니다.
/// </summary>
public class UISlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject equipEffect;

    private Item currentItem;

    /// <summary>
    /// 슬롯에 아이템 아이콘을 설정합니다.
    /// </summary>
    /// <param name="icon">아이템의 스프라이트 아이콘</param>
    public void SetItem(Item item)
    {
        currentItem = item;
        iconImage.sprite = item.data.icon;
        iconImage.enabled = iconImage.sprite != null;

        RefreshUI();
    }

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

    public void RefreshUI()
    {
        if (equipEffect != null)
        {
            equipEffect.SetActive(currentItem != null && currentItem.isEquipped);
        }
    }
}

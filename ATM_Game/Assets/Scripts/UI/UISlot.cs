using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리의 한 칸(슬롯)을 관리하는 UI 컴포넌트입니다.
/// </summary>
public class UISlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    /// <summary>
    /// 슬롯에 아이템 아이콘을 설정합니다.
    /// </summary>
    /// <param name="icon">아이템의 스프라이트 아이콘</param>
    public void SetItem(Sprite icon)
    {
        iconImage.sprite = icon;
        iconImage.enabled = icon != null;
    }

    /// <summary>
    /// 슬롯을 비웁니다.
    /// </summary>
    public void RefreshUI()
    {
        iconImage.sprite = null;
        iconImage.enabled = false;
    }
}

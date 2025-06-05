using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 인벤토리 UI 전체를 관리하는 클래스입니다.
/// </summary>
public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("슬롯 프리팹 및 슬롯 부모")]
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<UISlot> slots = new List<UISlot>();

    /// <summary>
    /// 인벤토리 UI 초기화
    /// </summary>
    private void Start()
    {
        InitInventoryUI();
    }

    private void OnEnable()
    {
        var character = GameManager.Instance.PlayerCharacter;

        if (character != null)
        {
            goldText.text = $"{character.gold:N0}";
        }
        else
        {
            Debug.LogWarning("캐릭터 정보가 없습니다.");
        }
    }

    /// <summary>
    /// 임시로 10개의 슬롯 생성하고 아이콘 설정
    /// </summary>
    private void InitInventoryUI()
    {
        for (int i = 0; i < 10; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slots.Add(slot);

            Sprite icon = Resources.Load<Sprite>("New Icons/PowerUp1");
            slot.SetItem(icon);
        }
    }
}

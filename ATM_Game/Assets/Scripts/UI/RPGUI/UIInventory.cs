using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// <b>인벤토리 UI 전체를 관리하는 클래스입니다.</b><br/>
/// - 플레이어 캐릭터의 인벤토리를 기반으로 UI 슬롯을 동적으로 생성합니다.<br/>
/// - 현재 보유 골드를 표시하고, 슬롯들을 자동으로 정렬 및 표시합니다.
/// </summary>
public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("슬롯 프리팹 및 슬롯 부모")]
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<UISlot> slots = new List<UISlot>();

    /// <summary>
    /// UIInventory가 활성화될 때 호출됩니다.<br/>
    /// - 캐릭터 정보를 바탕으로 골드를 표시하고 인벤토리 UI를 갱신합니다.
    /// </summary>
    private void OnEnable()
    {
        var character = GameManager.Instance.PlayerCharacter;

        if (character != null)
        {
            goldText.text = $"{character.gold:N0}";
            RefreshInventoryUI(character.Inventory.Items);
        }
        else
        {
            Debug.LogWarning("캐릭터 정보가 없습니다.");
        }
    }

    /// <summary>
    /// 인벤토리 데이터를 받아 슬롯 UI를 새로 생성합니다.<br/>
    /// - 기존 슬롯 오브젝트를 전부 제거한 뒤, 새로운 슬롯을 인벤토리 수만큼 생성합니다.
    /// </summary>
    /// <param name="inventory">현재 캐릭터가 보유한 아이템 리스트</param>
    private void RefreshInventoryUI(List<Item> inventory)
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        slots.Clear();

        foreach (var item in inventory)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.SetItem(item);
            slots.Add(slot);
        }
    }
}

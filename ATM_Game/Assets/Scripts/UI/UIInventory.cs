using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

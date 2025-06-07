using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// <b>메인 메뉴 UI를 관리하는 클래스입니다.</b><br/>
/// - 플레이어 이름, 레벨, 보유 골드를 화면에 표시합니다.<br/>
/// - 스탯 창, 인벤토리 창을 여는 버튼과 연결되어 있습니다.
/// </summary>
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("버튼 UI")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

    /// <summary>
    /// UI가 활성화될 때 호출되며, 플레이어 정보를 UI에 표시합니다.
    /// </summary>
    private void OnEnable()
    {
        var character = GameManager.Instance.PlayerCharacter;
        if (character != null)
        {
            nameText.text = character.characterName;
            levelText.text = $"LV.{character.level}";
            goldText.text = $"{character.gold:N0}";
        }
        else
        {
            Debug.LogWarning("캐릭터 정보가 없습니다.");
        }
    }

    /// <summary>
    /// 시작 시 버튼 이벤트 리스너를 등록합니다.
    /// </summary>
    private void Start()
    {
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    /// <summary>
    /// 상태창 UI를 활성화합니다.
    /// </summary>
    public void OpenStatus()
    {
        UIManager.Instance.ShowStatusCanvas();
    }

    /// <summary>
    /// 인벤토리 UI를 활성화합니다.
    /// </summary>
    public void OpenInventory()
    {
        UIManager.Instance.ShowInventoryCanvas();
    }
}

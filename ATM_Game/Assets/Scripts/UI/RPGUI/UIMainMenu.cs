using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("버튼 UI")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

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

    private void Start()
    {
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    public void OpenStatus()
    {
        UIManager.Instance.ShowStatusCanvas();
    }

    public void OpenInventory()
    {
        UIManager.Instance.ShowInventoryCanvas();
    }
}

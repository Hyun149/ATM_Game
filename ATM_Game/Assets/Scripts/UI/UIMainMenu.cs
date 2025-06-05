using UnityEngine;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;

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
}

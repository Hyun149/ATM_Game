using UnityEngine;
using TMPro;

/// <summary>
/// 캐릭터의 능력치를 UI에 표시하는 클래스입니다.
/// </summary>
public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;

    private void OnEnable()
    {
        var character = GameManager.Instance.PlayerCharacter;

        if (character != null)
        {
            statusText.text =
                $"공격력: {character.attack}\n" +
                $"방어력: {character.defense}\n" +
                $"체력: {character.health}";
        }
        else
        {
            statusText.text = "캐릭터 정보가 없습니다.";
            Debug.LogWarning("UIStatus: 캐릭터 정보가 null입니다.");
        }
    }

}

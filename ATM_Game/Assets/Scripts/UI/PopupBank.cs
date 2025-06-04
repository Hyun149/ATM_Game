using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupBank : MonoBehaviour
{
    [Header("잔액")]
    [SerializeField] private TextMeshProUGUI balanceText;

    [Header("Cash")]
    [SerializeField] private TextMeshProUGUI cashText;


    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var userData = GameManager.Instance.userData;

        balanceText.text = string.Format("{0:N0}", userData.balance);
        cashText.text = string.Format("{0:N0}", userData.cash);
    }
}

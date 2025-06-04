using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupBank : MonoBehaviour
{
    [Header("잔액")]
    [SerializeField] private TextMeshProUGUI balanceText;

    [Header("Cash")]
    [SerializeField] private TextMeshProUGUI cashText;

    private int lastCash;
    private int lastBalance;

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        var data = GameManager.Instance.userData;

        if (data.cash != lastCash || data.balance != lastBalance)
        {
            Refresh();
            lastCash = data.cash;
            lastBalance = data.balance;
        }
    }

    private void Refresh()
    {
        var userData = GameManager.Instance.userData;

        balanceText.text = string.Format("{0:N0}", userData.balance);
        cashText.text = string.Format("{0:N0}", userData.cash);
    }
}

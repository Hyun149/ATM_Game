using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupBank : MonoBehaviour
{
    [Header("잔액")]
    [SerializeField] private TextMeshProUGUI balanceText;

    [Header("Cash")]
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("직접 입력 필드")]
    [SerializeField] private TMP_InputField inputField;

    [Header("오류 팝업")]
    [SerializeField] private GameObject popupError;

    private int lastCash;
    private int lastBalance;

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        var data = GetUserData();

        if (data.cash != lastCash || data.balance != lastBalance)
        {
            Refresh();
            lastCash = data.cash;
            lastBalance = data.balance;
        }
    }

    private void Refresh()
    {
        var data = GetUserData();
        balanceText.text = $"{data.balance:N0}";
        cashText.text = $"{data.cash:N0}";
    }

    private UserData GetUserData() => GameManager.Instance.UserDataManager.CurrentUser;

    private void ShowError() => popupError?.SetActive(true);
    private void HideError() => popupError?.SetActive(false);
    private void CloseError() => HideError();

    public void Deposit(int amount) => ProcessTransation(amount, isDeposit: true);
    public void Withdraw(int amount) => ProcessTransation(amount, isDeposit: false);

    public void DepositFromInput() => HandleInput(isDeposit: true);
    public void WithdrawFromInput() => HandleInput(isDeposit: false);

    private void HandleInput(bool isDeposit)
    {
        if (int.TryParse(inputField.text, out int amount))
        {
            ProcessTransation(amount, isDeposit);
        }
        else
        {
            ShowError();
        }
    }

    private void ProcessTransation(int amount, bool isDeposit)
    {
        bool success = isDeposit
            ? GameManager.Instance.UserDataManager.TryDeposit(amount)
            : GameManager.Instance.UserDataManager.TryWithdraw(amount);

        if (success)
        {
            Refresh();
        }
        else
        {
            ShowError();
        }
    }
}

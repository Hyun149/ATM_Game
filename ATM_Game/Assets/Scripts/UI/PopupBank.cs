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

    /// <summary>
    /// 정해진 금액을 입금하는 버튼용 함수 (10,000 / 30,000 등)
    /// </summary>
    /// <param name="amount"></param>
    public void Deposit(int amount)
    {
        var data = GameManager.Instance.userData;

        if (amount > 0 && amount <= data.cash)
        {
            data.balance += amount;
            data.cash -= amount;
            Refresh();

            GameManager.Instance.SaveUserData();
        }
        else
        {
            popupError.SetActive(true);
        }
    }

    public void DepositFromInput()
    {
        var data = GameManager.Instance.userData;

        if (int.TryParse(inputField.text, out int inputAmount))
        {
            if (inputAmount > 0 && inputAmount <= data.cash)
            {
                data.balance += inputAmount;
                data.cash -= inputAmount;
                Refresh();

                GameManager.Instance.SaveUserData();
            }
            else
            {
                popupError.SetActive(true);
            }
        }
        else
        {
            popupError.SetActive(true);
        }
    }

    /// <summary>
    /// 정해진 금액을 출금하는 함수
    /// </summary>
    /// <param name="amount"> 출금할 금액</param>
    public void Withdraw(int amount)
    {
        var data = GameManager.Instance.userData;

        if (amount > 0 && amount <= data.balance)
        {
            data.balance -= amount;
            data.cash += amount;
            Refresh();

            GameManager.Instance.SaveUserData();
        }
        else
        {
            popupError.SetActive(true);
        }
    }

    public void WithdrawFromInput()
    {
        var data = GameManager.Instance.userData;

        if (int.TryParse(inputField.text, out int inputAmount))
        {
            if (inputAmount > 0 && inputAmount <= data.balance)
            {
                data.balance -= inputAmount;
                data.cash += inputAmount;
                Refresh();

                GameManager.Instance.SaveUserData();
            }
            else
            {
                popupError.SetActive(true);
            }
        }
        else
        {
            popupError.SetActive(true);
        }
    }

    /// <summary>
    /// 오류 팝업을 닫는다.
    /// </summary>
    public void CloseError()
    {
        popupError?.SetActive(false);
    }
}

using UnityEngine;
using TMPro;

/// <summary>
/// 은행 입출금 팝업 UI를 제어하는 클래스입니다.<br/>
/// - 유저 이름, 잔액, 현금 표시<br/>
/// - 입출금 처리 및 오류 팝업 제어
/// </summary>
public class PopupBank : MonoBehaviour
{
    [Header("유저 이름 텍스트")]
    [SerializeField] private TextMeshProUGUI userNameText;

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

    /// <summary>
    /// 시작 시 유저 데이터 갱신 이벤트를 등록하고 초기 UI를 갱신합니다.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.UserDataManager.OnUserDataChanged += Refresh;
        Refresh();
    }

    /// <summary>
    /// 오브젝트가 파괴될 때 이벤트를 해제하여 메모리 누수 방지
    /// </summary>
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UserDataManager.OnUserDataChanged -= Refresh;
        }
    }

    /// <summary>
    /// 유저 정보를 가져와 UI에 표시합니다.
    /// </summary>
    private void Refresh()
    {
        var data = GetUserData();
        balanceText.text = $"{data.balance:N0}";
        cashText.text = $"{data.cash:N0}";

        if (userNameText != null)
        {
            userNameText.text = data.userName;
        }
    }

    private UserData GetUserData() => GameManager.Instance.UserDataManager.CurrentUser;

    private void ShowError()
    {
        popupError?.SetActive(true);
        SFXManager.Instance.ErrorSound();
    }
    private void HideError() => popupError?.SetActive(false);
    private void CloseError() => HideError();

    public void Deposit(int amount) => ProcessTransation(amount, isDeposit: true);
    public void Withdraw(int amount) => ProcessTransation(amount, isDeposit: false);

    public void DepositFromInput() => HandleInput(isDeposit: true);
    public void WithdrawFromInput() => HandleInput(isDeposit: false);

    /// <summary>
    /// 입력된 문자열을 정수로 파싱하여 입출금을 시도합니다.<br/>
    /// 잘못된 입력이면 오류 팝업을 표시합니다.
    /// </summary>
    /// <param name="isDeposit">입금 여부</param>
    private void HandleInput(bool isDeposit)
    {
        if (int.TryParse(inputField.text, out int amount))
        {
            ProcessTransation(amount, isDeposit);
        }
        else
        {
            Debug.LogWarning("입출금 실패 에러!");
            ShowError();
        }
    }

    /// <summary>
    /// 실제 입출금 처리를 수행합니다.<br/>
    /// 성공 시 UI를 갱신하고, 실패 시 오류 팝업을 표시합니다.
    /// </summary>
    /// <param name="amount">처리할 금액</param>
    /// <param name="isDeposit">입금이면 true, 출금이면 false</param>
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
            Debug.LogWarning("입출금 실패!");
            ShowError();
        }
    }
}

using UnityEngine;
using TMPro;

/// <summary>
/// 유저 간 송금 기능을 담당하는 매니저 클래스입니다.<br/>
/// - 송금 대상 ID와 금액을 입력받아 유효성 검사를 수행한 뒤, 송금을 처리합니다.<br/>
/// - 처리 도중 오류가 발생하면 에러 메시지를 팝업으로 출력합니다.
/// </summary>
public class TransferManager : MonoBehaviour
{
    [Header("UI 입력 필드")]
    [SerializeField] private TMP_InputField inputTargetID;
    [SerializeField] private TMP_InputField inputAmount;

    [Header("에러 팝업")]
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TextMeshProUGUI errorText;

    /// <summary>
    /// UI에서 입력한 정보로 송금을 시도합니다.<br/>
    /// - 입력값 검증 → 대상 유저 찾기 → 잔액 확인 → 송금 처리 순으로 동작합니다.
    /// </summary>
    public void TryTransfer()
    {
        if (!ValidateInput(out string targetID, out int amount))
        {
            return;
        }

        if (!TryFindReceiver(targetID, out var receiver))
        {
            return;
        }

        if (!HasEnoughBalance(amount))
        {
            return;
        }

        ExecuteTransfer(receiver, amount);
    }

    /// <summary>
    /// 입력값이 유효한지 확인하고 결과를 반환합니다.
    /// </summary>
    /// <param name="targetID">송금 대상 ID (출력)</param>
    /// <param name="amount">송금 금액 (출력)</param>
    /// <returns>입력이 유효하면 true, 아니면 false</returns>
    private bool ValidateInput(out string targetID, out int amount)
    {
        targetID = inputTargetID.text.Trim();
        string amountText = inputAmount.text.Trim();

        if (string.IsNullOrEmpty(targetID) || string.IsNullOrEmpty(amountText))
        {
            ShowError("모든 항목을 입력해주세요.");
            SFXManager.Instance.ErrorSound();
            amount = 0;
            return false;
        }

        if (!int.TryParse(amountText, out amount) || amount <= 0)
        {
            ShowError("유효한 숫자를 입력해주세요.");
            SFXManager.Instance.ErrorSound();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 주어진 ID를 가진 유저 데이터를 조회합니다.
    /// </summary>
    /// <param name="targetID">송금 대상 ID</param>
    /// <param name="receiver">찾은 유저 데이터 (출력)</param>
    /// <returns>찾으면 true, 없으면 false</returns>
    private bool TryFindReceiver(string targetID, out UserData receiver)
    {
        receiver = GameManager.Instance.UserDataManager.AllUsers.Find(u => u.userID == targetID);
        if (receiver == null)
        {
            ShowError("해당 ID의 사용자를 찾을 수 없습니다.");
            SFXManager.Instance.ErrorSound();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 현재 유저의 잔액이 송금 가능한지 확인합니다.
    /// </summary>
    /// <param name="amount">송금할 금액</param>
    /// <returns>충분한 잔액이 있으면 true</returns>
    private bool HasEnoughBalance(int amount)
    {
        var sender = GameManager.Instance.UserDataManager.CurrentUser;
        if (sender.balance < amount)
        {
            ShowError("잔액이 부족합니다.");
            SFXManager.Instance.ErrorSound();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 송금을 실제로 수행합니다.<br/>
    /// - 보낸 사람의 잔액을 차감하고, 받는 사람의 잔액을 증가시킵니다.<br/>
    /// - 변경된 유저 데이터를 저장합니다.
    /// </summary>
    /// <param name="receiver">수신자 유저 데이터</param>
    /// <param name="amount">송금할 금액</param>
    private void ExecuteTransfer(UserData receiver, int amount)
    {
        var sender = GameManager.Instance.UserDataManager.CurrentUser;

        sender.balance -= amount;
        receiver.balance += amount;

        GameManager.Instance.UserDataManager.SaveUserData();
        Debug.Log($"송금 성공: {receiver.userID}에게 {amount}원 송금!");
    }

    /// <summary>
    /// 에러 메시지를 출력하고, 에러 팝업을 활성화합니다.
    /// </summary>
    /// <param name="message">표시할 에러 메시지</param>
    private void ShowError(string message)
    {
        Debug.LogWarning(message);
        if (errorText != null)
        {
            errorText.text = message;
        }

        if (errorPopup != null)
        {
            errorPopup.SetActive(true);
            SFXManager.Instance.ErrorSound();
        }
    }
}

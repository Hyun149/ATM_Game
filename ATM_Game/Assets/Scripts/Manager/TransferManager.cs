using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 유저 간 송금 기능을 담당하는 매니저 클래스입니다.<br/>
/// - 송금 대상 ID와 금액을 입력받아 유효성 검사를 수행한 뒤, 송금을 처리합니다.<br/>
/// - 에러 발생 시 에러 팝업을 표시합니다.
/// </summary>
public class TransferManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputTargetID;
    [SerializeField] private TMP_InputField inputAmount;
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TextMeshProUGUI errorText;

    /// <summary>
    /// 입력된 정보를 바탕으로 송금을 시도합니다.
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

    private bool ValidateInput(out string targetID, out int amount)
    {
        targetID = inputTargetID.text.Trim();
        string amountText = inputAmount.text.Trim();

        if (string.IsNullOrEmpty(targetID) || string.IsNullOrEmpty(amountText))
        {
            ShowError("모든 항목을 입력해주세요.");
            amount = 0;
            return false;
        }

        if (!int.TryParse(amountText, out amount) || amount <= 0)
        {
            ShowError("유효한 숫자를 입력해주세요.");
            return false;
        }

        return true;
    }

    private bool TryFindReceiver(string targetID, out UserData receiver)
    {
        receiver = GameManager.Instance.UserDataManager.AllUsers.Find(u => u.userID == targetID);
        if (receiver == null)
        {
            ShowError("해당 ID의 사용자를 찾을 수 없습니다.");
            return false;
        }

        return true;
    }

    private bool HasEnoughBalance(int amount)
    {
        var sender = GameManager.Instance.UserDataManager.CurrentUser;
        if (sender.balance < amount)
        {
            ShowError("잔액이 부족합니다.");
            return false;
        }

        return true;
    }

    private void ExecuteTransfer(UserData receiver, int amount)
    {
        var sender = GameManager.Instance.UserDataManager.CurrentUser;

        sender.balance -= amount;
        receiver.balance += amount;

        GameManager.Instance.UserDataManager.SaveUserData();
        Debug.Log($"송금 성공: {receiver.userID}에게 {amount}원 송금!");
    }

    /// <summary>
    /// 에러 메시지를 출력하고 에러 팝업을 활성화합니다.
    /// </summary>
    /// <param name="message">에러 내용</param>
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
        }
    }
}

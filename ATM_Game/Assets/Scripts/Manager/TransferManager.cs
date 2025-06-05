using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputTargetID;
    [SerializeField] private TMP_InputField inputAmount;
    [SerializeField] private GameObject errorPopup;

    public void TryTransfer()
    {
        string targetID = inputTargetID.text.Trim();
        string amountText = inputAmount.text.Trim();

        if (string.IsNullOrEmpty(targetID) || string.IsNullOrEmpty(amountText))
        {
            ShowError("모든 항목을 입력해주세요.");
            return;
        }

        if (!int.TryParse(amountText, out int amount) || amount <= 0)
        {
            ShowError("유효한 숫자를 입력해주세요.");
            return;
        }

        var sender = GameManager.Instance.UserDataManager.CurrentUser;
        var allUsers = GameManager.Instance.UserDataManager.UserList.users;

        if (sender.balance < amount)
        {
            ShowError("잔액이 부족합니다.");
            return;
        }

        // 수신자 찾기
        var receiver = allUsers.Find(u => u.userID == targetID);

        if (receiver == null)
        {
            ShowError("해당 ID의 사용자를 찾을 수 없습니다.");
            return;
        }

        // 송금 처리
        sender.balance -= amount;
        receiver.balance += amount;

        GameManager.Instance.UserDataManager.SaveUserData();
        Debug.Log($"송금 성공: {targetID}에게 {amount}원 송금!");
    }

    private void ShowError(string message)
    {
        Debug.LogWarning(message);
        if (errorPopup != null)
        {
            errorPopup.SetActive(true);
        }
    }
}

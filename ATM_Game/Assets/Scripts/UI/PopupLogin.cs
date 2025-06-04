using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    [Header("입력 필드")]
    [SerializeField] private TMP_InputField inputID;
    [SerializeField] private TMP_InputField inputPW;

    [Header("에러 메시지 UI")]
    [SerializeField] private GameObject popupError;

    [Header("전환할 UI")]
    [SerializeField] private GameObject popupBank;
    [SerializeField] private GameObject popupLogin;

    public void TryLogin()
    {
        string enteredID = inputID.text;
        string enteredPW = inputPW.text;

        var user = GameManager.Instance.UserDataManager.Data;

        if (enteredID.Trim() == user.userID && enteredPW.Trim() == user.password)
        {
            Debug.Log("로그인 성공!");
            popupBank.SetActive(true);
            popupLogin.SetActive(false);
        }
        else
        {
            Debug.Log("로그인 실패");
            popupError?.SetActive(true);
        }
    }
}

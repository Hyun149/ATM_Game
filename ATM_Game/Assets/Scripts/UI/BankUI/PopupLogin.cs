using TMPro;
using UnityEngine;

/// <summary>
/// 로그인 팝업 UI를 제어하는 클래스입니다.<br/>
/// - ID, 비밀번호 입력을 받아 로그인 시도<br/>
/// - 성공 시 UI 전환, 실패 시 에러 팝업 출력
/// </summary>
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

    /// <summary>
    /// 입력된 ID와 비밀번호로 로그인 시도<br/>
    /// - 성공 시 은행 팝업 UI로 전환<br/>
    /// - 실패 시 에러 팝업을 표시합니다.
    /// </summary>
    public void TryLogin()
    {
        string enteredID = inputID.text;
        string enteredPW = inputPW.text;

        var manager = GameManager.Instance.UserDataManager;

        if (manager.TryLogin(enteredID, enteredPW))
        {
            popupBank.SetActive(true);
            popupLogin.SetActive(false);
        }
        else
        {
            popupError?.SetActive(true);
            SFXManager.Instance.ErrorSound();
        }
    }
}

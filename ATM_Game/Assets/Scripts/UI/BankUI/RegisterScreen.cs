using UnityEngine;
using TMPro;

/// <summary>
/// 회원가입 화면을 제어하는 클래스입니다.<br/>
/// - 사용자 입력을 받아 새 유저 등록을 처리합니다.<br/>
/// - 입력값 검증, 중복 ID 체크, 비밀번호 확인 등 포함
/// </summary>
public class RegisterScreen : MonoBehaviour
{
    [Header("입력 필드")]
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputID;
    [SerializeField] private TMP_InputField inputPW;
    [SerializeField] private TMP_InputField inputPWCheck;

    [Header("에러 팝업")]
    [SerializeField] private GameObject popupError;

    [Header("전환할 UI")]
    [SerializeField] private GameObject popupLogin;
    [SerializeField] private GameObject registerScreen;

    /// <summary>
    /// 회원가입 버튼 클릭 시 호출되는 함수입니다.<br/>
    /// - 빈 칸 검사<br/>
    /// - 비밀번호 확인<br/>
    /// - 중복 ID 검사<br/>
    /// - 조건 충족 시 새 유저를 등록하고 초기 자금을 부여합니다.
    /// </summary>
    public void OnClickRegister()
    {
        string name = inputName.text.Trim();
        string id = inputID.text.Trim();
        string pw = inputPW.text.Trim();
        string pwCheck = inputPWCheck.text.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            Debug.LogWarning("빈 칸이 있습니다.");
            popupError.SetActive(true);
            return;
        }

        if (pw != pwCheck)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            popupError.SetActive(true);
            return;
        }


        if (GameManager.Instance.UserDataManager.IsDuplicateID(id))
        {
            Debug.LogWarning("이미 존재하는 ID입니다.");
            popupError?.SetActive(true);
            return;
        }

        UserData newUser = new UserData(name, id, pw, 50000, 50000);
        GameManager.Instance.UserDataManager.AddNewUser(newUser);

        Debug.Log("회원가입 완료!");

        popupLogin.SetActive(true);
        registerScreen.SetActive(false);
    }
}

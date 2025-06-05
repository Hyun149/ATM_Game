using UnityEngine;
using TMPro;

public class ResisterScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputID;
    [SerializeField] private TMP_InputField inputPW;
    [SerializeField] private TMP_InputField inputPWCheck;

    [SerializeField] private GameObject popupError;

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

        UserData newUser = new UserData(name, id, pw, 50000, 50000);

        if (GameManager.Instance.UserDataManager.IsDuplicateID(id))
        {
            Debug.LogWarning("이미 존재하는 ID입니다.");
            popupError?.SetActive(true);
            return;
        }

        GameManager.Instance.UserDataManager.AddnewUser(newUser);

        Debug.Log("회원가입 완료!");
    }
}

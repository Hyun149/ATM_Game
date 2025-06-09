using UnityEngine;

/// <summary>
/// <b>ATM 시뮬레이터 UI 전환을 관리하는 버튼 매니저 클래스입니다.</b><br/>
/// - 버튼 클릭 시 알맞은 UI 패널을 보여주고, 다른 패널은 숨깁니다.<br/>
/// - 각 기능(입금, 출금, 송금, 로그인 등)에 맞는 UI 활성화를 담당합니다.
/// </summary>
public class UIButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject atmUI;
    [SerializeField] private GameObject depositUI;
    [SerializeField] private GameObject withdrawUI;
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private GameObject transferUI;

    /// <summary>
    /// ATM 기본 화면을 보여줍니다.<br/>
    /// - 입금, 출금, 송금 패널은 비활성화하고 ATM UI만 활성화합니다.<br/>
    /// - 게임 데이터를 저장합니다.
    /// </summary>
    public void ShowATM()
    {
        GameManager.Instance.SaveGame();
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawUI.SetActive(false);
        transferUI.SetActive(false);
    }

    /// <summary>
    /// 입금 UI를 활성화합니다.<br/>
    /// - ATM, 출금, 송금 UI는 비활성화됩니다.
    /// </summary>
    public void ShowDepositUI()
    {
        depositUI.SetActive(true);
        atmUI.SetActive(false);
        withdrawUI.SetActive(false);
        transferUI.SetActive(false);
    }

    /// <summary>
    /// 출금 UI를 활성화합니다.<br/>
    /// - ATM과 입금 UI는 비활성화됩니다.
    /// </summary>
    public void ShowWithdrawUI()
    {
        withdrawUI.SetActive(true);
        atmUI.SetActive(false);
        depositUI.SetActive(false);
    }

    /// <summary>
    /// 로그인 UI를 활성화합니다.<br/>
    /// - 회원가입 UI는 비활성화됩니다.
    /// </summary>
    public void ShowLoginUI()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }

    /// <summary>
    /// 회원가입 UI를 활성화합니다.<br/>
    /// - 로그인 UI는 비활성화됩니다.
    /// </summary>
    public void ShowRegisterUI()
    {
        registerUI.SetActive(true);
        loginUI.SetActive(false);
    }

    /// <summary>
    /// 송금 UI를 활성화합니다.<br/>
    /// - ATM, 입금, 출금 UI는 비활성화됩니다.
    /// </summary>
    public void ShowTransferUI()
    {
        transferUI.SetActive(true);
        atmUI.SetActive(false);
        depositUI.SetActive(false);
        withdrawUI.SetActive(false);
    }

}

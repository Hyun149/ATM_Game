using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <b>UI 전반을 제어하는 싱글톤 UI 관리자 클래스입니다.</b><br/>
/// - 메인 메뉴, 은행, 상태창, 인벤토리 UI를 전환합니다.<br/>
/// - 각 UI는 필요한 시점에만 활성화되고, 나머지는 숨겨집니다.
/// </summary>
public class UIManager : MonoSingleton<UIManager>
{
    [Header("Canvas")]
    [SerializeField] private GameObject rpgMainMenuCanvas;
    [SerializeField] private GameObject bankUICanvas;
    [SerializeField] private GameObject statusCanvas;
    [SerializeField] private GameObject inventoryCanvas;

    /// <summary>
    /// 메인 메뉴 UI에 연결된 UI 스크립트 (필요 시 외부 접근 가능)
    /// </summary>
    public UIMainMenu MainMenuUI { get; private set; }

    /// <summary>
    /// RPG 메인 메뉴 UI를 표시합니다.<br/>
    /// - 현재 유저 데이터를 기반으로 캐릭터를 설정한 후 UI를 띄웁니다.<br/>
    /// - 다른 모든 UI는 숨깁니다.
    /// </summary>
    public void ShowRPGMainMenuCanvas()
    {
        var user = GameManager.Instance.UserDataManager.CurrentUser;
        GameManager.Instance.SetPlayerCharacter(user);
        HideAll();
        rpgMainMenuCanvas.SetActive(true);
        BGMManager.Instance.PlayRPGBGM();
    }

    /// <summary>
    /// 은행 UI를 표시합니다.<br/>
    /// - 저장 후 다른 UI를 숨기고 은행 UI만 활성화합니다.
    /// </summary>
    public void ShowBankCanvas()
    {
        GameManager.Instance.SaveGame();
        HideAll();
        bankUICanvas.SetActive(true);
        BGMManager.Instance.PlayBankBGM();
    }

    /// <summary>
    /// 상태창(UI)을 표시합니다.<br/>
    /// - 메인 메뉴 외에도 열릴 수 있습니다.
    /// </summary>
    public void ShowStatusCanvas()
    {
        statusCanvas.SetActive(true);
    }

    /// <summary>
    /// 상태창(UI)을 숨깁니다.
    /// </summary>
    public void HideStatusCanvas()
    {
        statusCanvas.SetActive(false);
    }

    /// <summary>
    /// 인벤토리 UI를 표시합니다.
    /// </summary>
    public void ShowInventoryCanvas()
    {
        inventoryCanvas.SetActive(true);
    }

    /// <summary>
    /// 인벤토리 UI를 숨깁니다.
    /// </summary>
    public void HideInventoryCanvas()
    {
        inventoryCanvas.SetActive(false);
    }

    /// <summary>
    /// 모든 주요 UI 캔버스를 비활성화합니다.
    /// </summary>
    private void HideAll()
    {
        rpgMainMenuCanvas.SetActive(false);
        bankUICanvas.SetActive(false);
        statusCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
    }
}

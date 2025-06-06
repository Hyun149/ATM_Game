using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Canvas")]
    [SerializeField] private GameObject rpgMainMenuCanvas;
    [SerializeField] private GameObject bankUICanvas;
    [SerializeField] private GameObject statusCanvas;
    [SerializeField] private GameObject inventoryCanvas;

    public UIMainMenu MainMenuUI { get; private set; }


    public void ShowRPGMainMenuCanvas()
    {
        var user = GameManager.Instance.UserDataManager.CurrentUser;
        GameManager.Instance.SetPlayerCharacter(user);

        HideAll();
        rpgMainMenuCanvas.SetActive(true);
    }

    public void ShowBankCanvas()
    {
        GameManager.Instance.SaveGame();
        HideAll();
        bankUICanvas.SetActive(true);
    }

    public void ShowStatusCanvas()
    {
        statusCanvas.SetActive(true);
    }

    public void HideStatusCanvas()
    {
        statusCanvas.SetActive(false);
    }

    public void ShowInventoryCanvas()
    {
        inventoryCanvas.SetActive(true);
    }

    public void HideInventoryCanvas()
    {
        inventoryCanvas.SetActive(false);
    }

    private void HideAll()
    {
        rpgMainMenuCanvas.SetActive(false);
        bankUICanvas.SetActive(false);
        statusCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject rpgMainMenuCanvas;
    [SerializeField] private GameObject bankUICanvas;

    public void ShowRPGMainMenuCanvas()
    {
        HideAll();
        rpgMainMenuCanvas.SetActive(true);
    }

    public void ShowBankCanvas()
    {
        HideAll();
        bankUICanvas.SetActive(true);
    }

    private void HideAll()
    {
        rpgMainMenuCanvas.SetActive(false);
        bankUICanvas.SetActive(false);
    }
}

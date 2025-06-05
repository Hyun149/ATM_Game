using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject atmUI;
    [SerializeField] private GameObject depositUI;
    [SerializeField] private GameObject withdrawUI;
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private GameObject transferUI;
    [SerializeField] private GameObject rpgMainUI;

    public void ShowATM()
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawUI.SetActive(false);
        transferUI.SetActive(false);
        rpgMainUI.SetActive(false);
    }

    public void ShowDepositUI()
    {
        depositUI.SetActive(true);
        atmUI.SetActive(false);
        withdrawUI.SetActive(false);
        transferUI.SetActive(false);
    }

    public void ShowWithdrawUI()
    {
        withdrawUI.SetActive(true);
        atmUI.SetActive(false);
        depositUI.SetActive(false);
        transferUI.SetActive(false);
    }

    public void ShowLoginUI()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }

    public void ShowRegisterUI()
    {
        registerUI.SetActive(true);
        loginUI.SetActive(false);
    }

    public void ShowTransferUI()
    {
        transferUI.SetActive(true);
        atmUI.SetActive(false);
        depositUI.SetActive(false);
        withdrawUI.SetActive(false);
    }

    public void ShowRPGMainUI()
    {
        rpgMainUI.SetActive(true);
        atmUI.SetActive(false);
    }
}

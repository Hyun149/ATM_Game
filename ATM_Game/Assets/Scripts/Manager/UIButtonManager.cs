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

    public void ShowATM()
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawUI.SetActive(false);
    }

    public void ShowDepositUI()
    {
        atmUI.SetActive(false);
        depositUI.SetActive(true);
        withdrawUI.SetActive(false);
    }

    public void ShowWithdrawUI()
    {
        atmUI.SetActive(false);
        depositUI.SetActive(false);
        withdrawUI.SetActive(true);
    }

    public void OnClickLoginUI()
    {
        registerUI.SetActive(false);
        loginUI.SetActive(true);
    }

    public void OnClickRegisterUI()
    {
        registerUI.SetActive(true);
        loginUI.SetActive(false);
    }
}

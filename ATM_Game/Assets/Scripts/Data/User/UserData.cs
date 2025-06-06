using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName;
    public string userID;
    public string password;
    public int cash;
    public int balance;
    

    public UserData(string userName, string id, string pw,int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
        this.userID = id;
        this.password = pw;
    }
}

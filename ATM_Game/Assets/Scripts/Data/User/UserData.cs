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
    
    public List<ItemSaveData> inventory = new List<ItemSaveData>();

    public UserData(string userName, string id, string pw,int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
        this.userID = id;
        this.password = pw;
    }

    public void SaveFromCharacter(Character character)
    {
        this.inventory.Clear();
        foreach (var item in character.Inventory.Items)
        {
            Debug.Log($"[저장]: {item.data.name} / 장착: {item.isEquipped}");
            inventory.Add(new ItemSaveData(item.data.name, item.isEquipped));
        }
    }
}

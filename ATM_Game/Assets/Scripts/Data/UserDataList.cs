using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserDataList
{
    public List<UserData> users;

    public UserDataList()
    {
        users = new List<UserData>();
    }
}

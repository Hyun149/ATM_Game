using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserDataManager
{
    void Init();
    void SaveUserData();
    UserData CurrentUser { get; }
}

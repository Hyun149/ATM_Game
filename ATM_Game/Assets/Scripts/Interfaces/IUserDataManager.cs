using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserDataManager
{
    void Init();
    void SaveUserData();
    UserData CurrentUser { get; }

    bool TryLogin(string id, string pw);
    bool TryDeposit(int amount);
    bool TryWithdraw(int amount);
    bool IsDuplicateID(string id);

    void AddNewUser(UserData newUser);

    event System.Action OnUserDataChanged;

    List<UserData> AllUsers { get; }
}

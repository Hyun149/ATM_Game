using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class UserDataManager
{
    public event Action OnUserDataChanged;

    private const string FileName = "saveData.json";
    private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    public UserDataList UserList { get; private set; } = new UserDataList();
    public UserData CurrentUser { get; private set; }


    public void Init()
    {
        LoadUserData();
    }

    public bool IsDuplicateID(string id)
    {
        return UserList.users.Any(u => u.userID == id);
    }

    public void AddNewUser(UserData newUser)
    {
        UserList.users.Add(newUser);
        SaveUserData();
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(UserList, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadUserData()
    {
        if (File.Exists(SavePath))
        {
            try
            {
            string json = File.ReadAllText(SavePath);
            UserList = JsonUtility.FromJson<UserDataList>(json);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"유저 데이터 로드 실패: {ex.Message}");
            }

            Debug.Log("유저 데이터 로드 완료");

            if (UserList.users.Count > 0)
            {

                // AddTestUser(); // 테스트 계정

                CurrentUser ??= UserList.users[0];
            }
            else
            {
                // 완전 빈 리스트인 경우 테스트 유저 추가
                AddTestUser();
            }
        }
        else
        {
            UserList = new UserDataList();
            Debug.Log("저장 파일 없음!, 기본값 사용");
        }
    }

    /// <summary>
    /// 입금 처리: 현금 -> 잔액
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryDeposit(int amount)
    {
        if (amount > 0 && amount <= CurrentUser.cash)
        {
            CurrentUser.cash -= amount;
            CurrentUser.balance += amount;
            SaveUserData();
            OnUserDataChanged?.Invoke();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 출금 처리: 잔액 -> 현금
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryWithdraw(int amount)
    {
        if (amount > 0 && amount <= CurrentUser.balance)
        {
            CurrentUser.balance -= amount;
            CurrentUser.cash += amount;
            SaveUserData();
            OnUserDataChanged?.Invoke();
            return true;
        }

        return false;
    }

    /*public void SetUserData(UserData data)
    {
        this.CurrentUser = data;
    }*/

    public bool TryLogin(string id, string pw)
    {
        foreach (var user in UserList.users)
        {
            if (user.userID == id && user.password == pw)
            {
                CurrentUser = user;
                return true;
            }
        }

        return false;
    }

    private void AddTestUser()
    {
        var testUser = new UserData("GM 현", "0000", "0000", 9999999, 11111111);
        UserList.users.Add(testUser);
        CurrentUser = testUser;
        SaveUserData();
    }
}

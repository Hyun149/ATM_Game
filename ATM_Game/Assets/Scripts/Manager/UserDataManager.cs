using UnityEngine;
using System.IO;
using System.Linq;

public class UserDataManager
{
    private const string FileName = "saveData.json";
    private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    public UserDataList UserList { get; private set; } = new UserDataList();
    public UserData CurrentUser { get; private set; }

    public bool IsDuplicateID(string id)
    {
        LoadUserData();
        return UserList.users.Any(u => u.userID == id);
    }

    public void AddnewUser(UserData newUser)
    {
        UserList.users.Add(newUser);
        SaveUserData();
    }

    public void SaveUserData()
    {
        string Json = JsonUtility.ToJson(UserList, true);
        File.WriteAllText(SavePath, Json);
    }

    public void LoadUserData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            UserList = JsonUtility.FromJson<UserDataList>(json);
            Debug.Log("유저 데이터 로드 완료");
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
            return true;
        }

        return false;
    }

    public void SetUserData(UserData data)
    {
        this.CurrentUser = data;
    }

    public bool TryLogin(string id, string pw)
    {
        LoadUserData();

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
}

using UnityEngine;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

/// <summary>
/// <b>유저 데이터 저장/불러오기 및 입출금, 로그인 처리를 담당하는 관리자 클래스입니다.</b><br/>
/// - Json 파일 기반으로 유저 데이터를 직렬화 및 역직렬화합니다.<br/>
/// - 입금/출금/로그인 로직 포함<br/>
/// - 초기 등록 시 시작 아이템을 자동 지급하며, 데이터 변경 시 이벤트를 발생시킵니다.
/// </summary>
public class UserDataManager : IUserDataManager
{
    public event Action OnUserDataChanged;

    /// <summary> 모든 유저 리스트를 담고 있는 데이터 객체입니다. </summary>
    public UserDataList UserList { get; private set; } = new UserDataList();

    /// <summary> 전체 유저 목록을 리스트 형태로 반환합니다. </summary>
    public List<UserData> AllUsers => UserList.users;

    /// <summary> 현재 로그인된 유저 데이터입니다. </summary>
    public UserData CurrentUser { get; private set; }

    private const string FileName = "saveData.json";

    /// <summary> 실제 저장되는 경로입니다. </summary>
    private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    /// <summary>
    /// UserDataManager를 초기화하며, 저장된 데이터를 불러옵니다.
    /// </summary>
    public void Init()
    {
        LoadUserData();
    }

    /// <summary>
    /// 중복된 ID가 존재하는지 확인합니다.
    /// </summary>
    /// <param name="id">검사할 ID 문자열</param>
    /// <returns>중복이면 true, 아니면 false</returns>
    public bool IsDuplicateID(string id)
    {
        return UserList.users.Any(u => u.userID == id);
    }

    /// <summary>
    /// 신규 유저를 추가합니다.<br/>
    /// - 초기 캐릭터 및 아이템을 생성하여 저장합니다.
    /// </summary>
    /// <param name="newUser">신규 유저 데이터</param>
    public void AddNewUser(UserData newUser)
    {
        UserList.users.Add(newUser);
        CurrentUser = newUser;

        // 캐릭터 생성 후 시작 아이템 지급
        var character = new Character(newUser);
        foreach (var item in GameManager.Instance.initialItems)
        {
            character.AddItem(item);
        }

        // 인벤토리 저장 후 파일 저장
        newUser.SaveFromCharacter(character);
        SaveUserData();
    }

    /// <summary>
    /// 유저 데이터를 JSON 파일로 저장합니다.
    /// </summary>
    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(UserList, true);
        File.WriteAllText(SavePath, json);
    }

    /// <summary>
    /// 저장된 JSON 파일에서 유저 데이터를 불러옵니다.<br/>
    /// - 없으면 기본값 또는 테스트 유저를 생성합니다.
    /// </summary>
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
    /// 입금 처리: 현금에서 잔액으로 자금을 이동합니다.
    /// </summary>
    /// <param name="amount">입금할 금액</param>
    /// <returns>입금 성공 여부</returns>
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
    /// 출금 처리: 잔액에서 현금으로 자금을 이동합니다.
    /// </summary>
    /// <param name="amount">출금할 금액</param>
    /// <returns>출금 성공 여부</returns>
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

    /// <summary>
    /// 로그인 시도: ID와 비밀번호가 일치하는 유저를 찾습니다.
    /// </summary>
    /// <param name="id">입력한 ID</param>
    /// <param name="pw">입력한 비밀번호</param>
    /// <returns>로그인 성공 여부</returns>
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

    /// <summary>
    /// 개발 테스트용 유저를 추가합니다.<br/>
    /// - 저장 파일이 없거나 유저 리스트가 비어있을 때 사용됩니다.
    /// </summary>
    private void AddTestUser()
    {
        var testUser = new UserData("GM 현", "0000", "0000", 9999999, 11111111);
        UserList.users.Add(testUser);
        CurrentUser = testUser;
        SaveUserData();
    }
}

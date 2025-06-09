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
    public UserDataList UserList { get; private set; }
    public List<UserData> AllUsers => UserList.users;
    public UserData CurrentUser { get; private set; }
    public event Action OnUserDataChanged;

    private readonly IUserDataStorage storage;
    private readonly CharacterFactory characterFactory;

    /// <summary>
    /// UserDataManager를 생성합니다.<br/>
    /// - 저장소와 캐릭터 팩토리를 주입받아 사용합니다.
    /// </summary>
    /// <param name="storage">유저 데이터 저장소 구현체</param>
    /// <param name="factory">캐릭터 팩토리</param>
    public UserDataManager(IUserDataStorage storage, CharacterFactory factory)
    {
        this.storage = storage;
        this.characterFactory = factory;
    }

    /// <summary>
    /// UserDataManager를 초기화하며, 저장된 유저 데이터를 불러옵니다.<br/>
    /// - 저장된 데이터가 없으면 테스트 유저를 자동 추가합니다.
    /// </summary>
    public void Init()
    {
        UserList = storage.Load();

        if (UserList.users.Count == 0)
        {
            AddTestUser();
        }
        else
        {
            CurrentUser ??= UserList.users[0];
        }
    }

    /// <summary>
    /// 주어진 ID가 이미 존재하는지 확인합니다.
    /// </summary>
    /// <param name="id">검사할 유저 ID</param>
    /// <returns>중복 여부</returns>
    public bool IsDuplicateID(string id) => UserList.users.Any(u => u.userID == id);

    /// <summary>
    /// 신규 유저를 추가합니다.<br/>
    /// - 초기 캐릭터 및 아이템을 생성하여 저장합니다.
    /// </summary>
    /// <param name="newUser">신규 유저 데이터</param>
    public void AddNewUser(UserData newUser)
    {
        var character = characterFactory.CreateCharacter(newUser);
        newUser.SaveFromCharacter(character);

        UserList.users.Add(newUser);
        CurrentUser = newUser;

        storage.Save(UserList);
        OnUserDataChanged?.Invoke();
    }

    /// <summary>
    /// 로그인 시도: ID와 비밀번호가 일치하는 유저를 찾습니다.
    /// </summary>
    /// <param name="id">입력한 ID</param>
    /// <param name="pw">입력한 비밀번호</param>
    /// <returns>로그인 성공 여부</returns>
    public bool TryLogin(string id, string pw)
    {
        var user = UserList.users.FirstOrDefault(u => u.userID == id && u.password == pw);
        if (user == null)
        {
            return false;
        }

        CurrentUser = user;
        return true;
    }

    /// <summary>
    /// 입금 처리: 현금에서 잔액으로 자금을 이동합니다.
    /// </summary>
    /// <param name="amount">입금할 금액</param>
    /// <returns>입금 성공 여부</returns>
    public bool TryDeposit(int amount)
    {
        if (CurrentUser == null || amount <= 0 || amount > CurrentUser.cash)
        {
            return false;
        }

        CurrentUser.cash -= amount;
        CurrentUser.balance += amount;
        storage.Save(UserList);
        OnUserDataChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// 출금 처리: 잔액에서 현금으로 자금을 이동합니다.
    /// </summary>
    /// <param name="amount">출금할 금액</param>
    /// <returns>출금 성공 여부</returns>
    public bool TryWithdraw(int amount)
    {
        if (CurrentUser == null || amount <= 0 || amount > CurrentUser.balance)
        {
            return false;
        }

        CurrentUser.balance -= amount;
        CurrentUser.cash += amount;
        storage.Save(UserList);
        OnUserDataChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// 현재 유저 데이터를 저장합니다.<br/>
    /// - 수동 저장을 원할 때 호출합니다.
    /// </summary>
    public void SaveUserData()
    {
        storage.Save(UserList);
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
        storage.Save(UserList);
    }
}

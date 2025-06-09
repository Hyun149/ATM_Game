using System.Collections.Generic;

/// <summary>
/// 유저 데이터 관련 기능을 정의하는 인터페이스입니다.<br/>
/// - 로그인, 입금/출금, 유저 추가 및 저장 기능을 정의합니다.
/// - 게임 내 다양한 시스템이 유저 데이터에 의존하지 않고 유연하게 접근할 수 있도록 도와줍니다.
public interface IUserDataManager
{
    void Init();
    void SaveUserData();
    void AddNewUser(UserData newUser);

    UserData CurrentUser { get; }
    List<UserData> AllUsers { get; }

    bool TryLogin(string id, string pw);
    bool TryDeposit(int amount);
    bool TryWithdraw(int amount);
    bool IsDuplicateID(string id);

    event System.Action OnUserDataChanged;
}

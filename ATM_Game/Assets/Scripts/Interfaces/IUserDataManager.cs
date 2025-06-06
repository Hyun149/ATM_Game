using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저 데이터 관련 기능을 정의하는 인터페이스입니다.<br/>
/// - 로그인, 입금/출금, 유저 추가 및 저장 기능을 정의합니다.
/// - 게임 내 다양한 시스템이 유저 데이터에 의존하지 않고 유연하게 접근할 수 있도록 도와줍니다.
public interface IUserDataManager
{
    /// <summary>
    /// 유저 데이터 매니저를 초기화합니다.<br/>
    /// - 저장된 유저 데이터를 불러옵니다.
    /// </summary>
    void Init();

    /// <summary>
    /// 현재 유저 데이터를 저장합니다.<br/>
    /// - JSON 등으로 직렬화하여 파일에 저장할 수 있습니다.
    /// </summary>
    void SaveUserData();

    /// <summary>
    /// 현재 로그인 중인 유저 정보를 반환합니다.
    /// </summary>
    UserData CurrentUser { get; }

    /// <summary>
    /// 주어진 ID와 비밀번호로 로그인을 시도합니다.
    /// </summary>
    /// <param name="id">입력한 유저 ID</param>
    /// <param name="pw">입력한 비밀번호</param>
    /// <returns>로그인 성공 여부</returns>
    bool TryLogin(string id, string pw);

    /// <summary>
    /// 유저 계좌에 금액을 입금합니다.
    /// </summary>
    /// <param name="amount">입금할 금액</param>
    /// <returns>입금 성공 여부</returns>
    bool TryDeposit(int amount);

    /// <summary>
    /// 유저 계좌에서 금액을 출금합니다.
    /// </summary>
    /// <param name="amount">출금할 금액</param>
    /// <returns>출금 성공 여부</returns>
    bool TryWithdraw(int amount);

    /// <summary>
    /// 주어진 ID가 이미 존재하는지 확인합니다.
    /// </summary>
    /// <param name="id">검사할 유저 ID</param>
    /// <returns>중복 여부</returns>
    bool IsDuplicateID(string id);

    /// <summary>
    /// 새로운 유저 데이터를 추가합니다.
    /// </summary>
    /// <param name="newUser">추가할 유저 데이터</param>
    void AddNewUser(UserData newUser);

    /// <summary>
    /// 전체 유저 데이터 리스트를 반환합니다.
    /// </summary>
    List<UserData> AllUsers { get; }

    /// <summary>
    /// 유저 데이터가 변경되었을 때 발생하는 이벤트입니다.<br/>
    /// - UI 등의 요소가 이 이벤트를 구독하여 자동 갱신이 가능합니다.
    /// </summary>
    event System.Action OnUserDataChanged;
}

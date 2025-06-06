using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체 유저 정보를 저장하는 리스트 클래스입니다.<br/>
/// - 여러 유저의 정보를 JSON으로 저장/불러오기 위해 사용됩니다.
/// </summary>
[System.Serializable]
public class UserDataList
{
    public List<UserData> users;

    /// <summary>
    /// 빈 유저 리스트를 생성합니다.
    /// </summary>
    public UserDataList()
    {
        users = new List<UserData>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 데이터를 담는 클래스입니다.
/// </summary>
[System.Serializable]
public class Character
{
    public string characterName;
    public int level;
    public int gold;

    public Character(UserData user)
    {
        this.characterName = user.userName;
        this.level = Mathf.Max(1, user.balance / 37145); //최소 1레벨 보장
        this.gold = user.balance / 34;
    }
}

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
        this.level = 1; // 레벨은 아직 X
        this.gold = user.balance / 10;
    }
}

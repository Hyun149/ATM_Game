using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 정보를 저장하는 데이터 클래스입니다.
/// - 이름, 아이디, 비밀번호, 보유 현금/잔액 등을 포함합니다.
/// - 인벤토리 정보도 함께 직렬화할 수 있습니다.
/// </summary>
[System.Serializable]
public class UserData
{
    public string userName;
    public string userID;
    public string password;
    public int cash;
    public int balance;
    
    public List<ItemSaveData> inventory = new List<ItemSaveData>();

    /// <summary>
    /// UserData 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="userName">유저 이름</param>
    /// <param name="id">유저 ID</param>
    /// <param name="pw">유저 비밀번호</param>
    /// <param name="cash">보유 현금</param>
    /// <param name="balance">보유 예치금</param
    public UserData(string userName, string id, string pw,int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
        this.userID = id;
        this.password = pw;
    }

    /// <summary>
    /// 캐릭터 데이터를 기반으로 현재 유저의 인벤토리 정보를 저장합니다.
    /// - 기존 인벤토리를 클리어하고, 장착 여부와 함께 아이템 정보를 기록합니다.
    /// </summary>
    /// <param name="character">저장할 대상 캐릭터 객체</param>
    public void SaveFromCharacter(Character character)
    {
        this.inventory.Clear();
        foreach (var item in character.Inventory.Items)
        {
            inventory.Add(new ItemSaveData(item.data.name, item.isEquipped));
        }
    }
}

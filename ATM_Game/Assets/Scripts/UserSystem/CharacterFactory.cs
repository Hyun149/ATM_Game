using System.Collections.Generic;

/// <summary>
/// <b>캐릭터 생성을 담당하는 팩토리 클래스입니다.</b><br/>
/// - 유저 데이터를 기반으로 캐릭터 인스턴스를 생성합니다.<br/>
/// - 생성 시 초기 아이템들을 자동으로 지급합니다.
/// </summary>
public class CharacterFactory
{
    private readonly List<ItemData> initialItems;

    /// <summary>
    /// 생성자: 초기 아이템 리스트를 받아 팩토리를 초기화합니다.
    /// </summary>
    /// <param name="initialItems">캐릭터 생성 시 지급할 초기 아이템들</param>
    public CharacterFactory(List<ItemData> initialItems)
    {
        this.initialItems = initialItems;
    }

    /// <summary>
    /// 주어진 유저 데이터를 기반으로 캐릭터를 생성합니다.<br/>
    /// - 생성된 캐릭터는 초기 아이템을 자동으로 보유하게 됩니다.
    /// </summary>
    /// <param name="user">캐릭터에 대응할 유저 데이터</param>
    /// <returns>초기화된 캐릭터 인스턴스</returns>
    public Character CreateCharacter(UserData user)
    {
        var character = new Character(user);

        foreach (var item in initialItems)
        {
            character.AddItem(item);

        }
        return character;
    }
}

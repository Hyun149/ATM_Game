using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    private readonly List<ItemData> initialItems;

    public CharacterFactory(List<ItemData> initialItems)
    {
        this.initialItems = initialItems;
    }

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

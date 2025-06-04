using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateHandler
{
    public static void Handle(GameState state)
    {
        switch (state)
        {
            case GameState.Title:
                break;
            case GameState.InGame:
                break;
        }
    }
}

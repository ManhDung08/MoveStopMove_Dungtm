using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    MainMenu,
    InGame,
    Pause,
    ShopSkinMenu,
    ShopWeaponMenu,
    EndGame
}
public class GameManager : Singleton<GameManager>
{
    private int maxNumofBots = 20;

    public GameState currentState;

    private void Start()
    {
        currentState = GameState.MainMenu;
    }
}

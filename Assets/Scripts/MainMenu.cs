using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static float backgroundTrashSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        ResetGameStateClass();
        GameState.isInMenu = true;
    }

    public static void ResetGameStateClass(PlayerController player = null, int level = 0)
    {
        GameState.score = 0;
        GameState.currentLives = GameState.maxLives;
        GameState.level = level;
        GameState.player = player;
        GameState.gameLoopActive = true;
        GameState.isInMenu = false;
    }
}

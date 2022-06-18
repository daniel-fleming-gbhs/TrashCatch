using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // The speed of the trash shown in the background.
    public static float backgroundTrashSpeed = 2;

    // The settings menu object.
    public GameObject settingsMenu;

    // The snapshots of the audio mixer for paused and playing.
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot playing;

    // Start is called before the first frame update
    void Start()
    {
        // If the scene is an end scene (ie. win loss), fade the text out.
        if (gameObject.name == "EndController")
        {
            GameObject.Find("UI/EndText").GetComponent<FadeImage>().FadeOutImage();
        }

        // Reset the game state class to defaults.
        ResetGameStateClass();
        GameState.isInMenu = true;
        GameState.level = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Flip the active boolean of the settings menu.
            settingsMenu.SetActive(!settingsMenu.activeSelf);

            // Set the time scale to 1 if 0 and vice versa, and transition to an audio snapshot.
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                playing.TransitionTo(0);
            }
            else
            {
                Time.timeScale = 0;
                paused.TransitionTo(0);
            }
        }
    }

    // Resets the game state class.
    public static void ResetGameStateClass(PlayerController player = null)
    {
        GameState.score = 0;
        GameState.currentLives = GameState.maxLives;
        GameState.player = player;
        GameState.gameLoopActive = true;
        GameState.isInMenu = false;
    }
}

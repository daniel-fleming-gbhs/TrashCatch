public static class GameState
{
    // The starting amount of lives the player has used for some calcuations.
    public const int maxLives = 3;
    
    // The player's current score.
    public static int score;

    public static int level = 0;
    
    // Stores the player current lives.
    public static int currentLives;

    // The win condition score.
    public static int targetScore => 2 * GameState.level + 20;

    // The minimum speed the trash can move at.
    public static float minTrashSpeed => 0.1f * GameState.level + 2;

    // The maximum speed the trash can move at.
    public static float maxTrashSpeed => 0.2f * GameState.level + 4;

    public static float trashSpawnChance => Minimum(0.05f * GameState.level + 0.3f, 1); 

    // The instance of the player controller so all scripts can reference it
    // without needing to be passed the instance directly.
    public static PlayerController player;

    // Whether or not the game loop is active.
    public static bool gameLoopActive;

    // Whether or not the game is in the main menu.
    public static bool isInMenu;

    private static float Minimum(float one, float two)
    {
        if (one < two)
        {
            return one;
        }
        else
        {
            return two;
        }
    }
}

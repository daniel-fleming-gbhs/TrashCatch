public static class GameState
{
    // The starting amount of lives the player has used for some calcuations.
    public const int maxLives = 3; // ****currently unused****
    
    // The player's current score.
    public static int score = 0;
    
    // Stores the player current lives, defaulting to 3.
    public static int currentLives = 3;

    // The win condition score.
    public static int targetScore = 20;

    // The minimum speed the trash can move at.
    public static float minTrashSpeed = 2;

    // The maximum speed the trash can move at.
    public static float maxTrashSpeed = 5;

    // The instance of the player controller so all scripts can reference it
    // without needing to be passed the instance directly.
    public static PlayerController player;

    // Whether or not the game loop is active.
    public static bool gameLoopActive = true;
}

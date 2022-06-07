public static class GameState
{
    // The starting amount of lives the player has used for some calcuations
    public const int MaxLives = 3;
    // The variable for storing the player lives, defaulting to 3
    public static int Score = 0;
    // The variable for storing the player lives, defaulting to 3
    public static int Lives = 3;
    public static int TargetScore = 20;

    // Variables for the min and max speed that the trash can move at
    public static float MaxTrashSpeed = 2;
    public static float MinTrashSpeed = 5;

    public static PlayerController player;
}

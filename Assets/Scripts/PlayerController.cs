using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    // The rigidbody component of the character.
    private Rigidbody2D playerRigidBody2D;

    // The game object for the UI that contains the win screen.
    private UIAudioCollection winCollection;

    // The game object for the UI that contains the fail screen.
    private UIAudioCollection failCollection;
    private GameObject failUIText;
    
    // The (constant) maximum speed the player can travel. This is used to cap acceleration.
    private const float maxMovementSpeed = 2.5f;

    // The (constant) movement speed multiplier to scale up the forces applied to the character.
    private const float moveSpeedMultiplier = 1000;

    // The (constant) minimum distance between where the cursor is and the player, for the player to move.
    private const float minimumMouseMoveDistance = 0.2f;

    public GameObject settingsMenu;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot playing;

    // Start is a method that is called before the first frame update.
    private void Start()
    {
        // Get and set the playerRigidBody2D variable to the instance of the rigidbody.
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        
        // Set the player variable in the (static) GameState class, so that all functions can access this
        // instance of this class.
        MainMenu.ResetGameStateClass(this);

        // Gets and sets the UI and Audio for the win and fail screens.
        winCollection = new UIAudioCollection("UI/Win", "UI/Win/LevelDigits", "Audio/Music");
        winCollection.panel.SetActive(false);
        failCollection = new UIAudioCollection("UI/Fail", "UI/Fail/LevelDigits", "Audio/Music");
        failCollection.panel.SetActive(false);
    }

    // FixedUpdate is a method that is called once each frame.
    private void FixedUpdate()
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

        // Do not execute the rest of the code if the game loop isn't active.
        if (!GameState.gameLoopActive)
            return;

        // A variable for adding forces to the player rigidbody.
        Vector2 movementForce = new Vector2();

        // Gets the keys inputted and puts them in a vector.
        movementForce.x = Input.GetAxis("Horizontal");
        movementForce.y = Input.GetAxis("Vertical");

        // Add this force to the rigidbody to move the player and multiply it by the movement speed multiplier.
        playerRigidBody2D.AddForce(movementForce * moveSpeedMultiplier);

        // Check if the left mouse button is being pressed.
        if (Input.GetMouseButton(0))
        {            
            // Get the mouse cursor position and convert it to world co-ordinates instead of pixel co-ordinates.
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Calculates the difference and distance between the mouse and player
            Vector2 mouseDifferenceFromPlayer = (Vector2)(transform.position - target);
            float mouseDistanceFromPlayer = VectorMagnitude(mouseDifferenceFromPlayer);
            
            // If the vector is not NaN or Infinity, and if the distance between the player and mouse is greater
            // than or equal to the minimumMouseMoveDistance.
            if (VectorIsNormal(mouseDifferenceFromPlayer) && mouseDistanceFromPlayer >= minimumMouseMoveDistance)
            {
                // Calculates the force and adds it to the player
                movementForce = -Normalize(mouseDifferenceFromPlayer);
                playerRigidBody2D.AddForce(movementForce * moveSpeedMultiplier);
            }
        }

        // Clamps the velocity vector of the player with the maximum movement speed.
        Vector2 newClampedVelocity = new Vector2();
        newClampedVelocity.x = Mathf.Clamp(playerRigidBody2D.velocity.x, -maxMovementSpeed, maxMovementSpeed);
        newClampedVelocity.y = Mathf.Clamp(playerRigidBody2D.velocity.y, -maxMovementSpeed, maxMovementSpeed);
        playerRigidBody2D.velocity = newClampedVelocity;
    }

    // DoFail is a method that runs when the player loses, when the player loses all three lives.
    public void DoFail()
    {
        // The player has lost so reset the level to 0, so the game can be restarted.
        GameState.level = 0;

        // Runs end of level function with a reference to the failCollection.
        FadeImage fadeObject = GameObject.Find("UI/FadePanel").GetComponent<FadeImage>();
        fadeObject.sceneName = "GameOver";
        EndOfLevel(ref failCollection, fadeObject);
    }

    // DoWin is a method that runs when the player wins, when the player's score reaches the target score.
    public void DoLevelWin()
    {
        // Run seperate function and exit this function if the player has reached level 40 (at the end of a level).
        if (GameState.level + 1 > 39)
        {
            DoGameCompleted();
            return;
        }

        // Runs end of level function with a reference to the winCollection.
        FadeImage fadeObject = GameObject.Find("UI/FadePanel").GetComponent<FadeImage>();
        fadeObject.sceneName = "GameScene";
        EndOfLevel(ref winCollection, fadeObject);
    }

    // Generic end of level function that runs to transition to the next level / fail screen.
    public void EndOfLevel(ref UIAudioCollection collection, FadeImage fadeObject)
    {
        // Find the scene's instance of the object spawner game object and sets it to inactive so all leftover objects can no
        // longer be seen.
        GameObject objectSpawner = GameObject.Find("Object Spawner");
        objectSpawner.SetActive(false);

        // Disable the game loop so the player can no longer move and no more objects spawn.
        GameState.gameLoopActive = false;

        // Sets the UI to active and fades it in.
        collection.panel.SetActive(true);
        collection.uiFadeCollection.FadeIn();

        // Increment the level and display it in the UI by updating it.
        GameState.level++;
        collection.digitsNumberHandler.UpdateNumbers(GameState.level);

        // Update the fade speeds to match and increases them.
        fadeObject.fadeSpeed += 2;
        collection.musicFade.fadeSpeed = fadeObject.fadeSpeed;

        // Fade in the black screen object, and fade the music out.
        fadeObject.FadeInImage();
        collection.musicFade.FadeOutAudio();
    }

    // Runs when the player has one the game.
    public void DoGameCompleted()
    {
        // The player has won so reset the level to 0, so the game can be restarted.
        GameState.level = 0;
        
        // Runs end of level function with a reference to the winCollection.
        FadeImage fadeObject = GameObject.Find("UI/FadePanel").GetComponent<FadeImage>();
        fadeObject.sceneName = "GameComplete";
        EndOfLevel(ref winCollection, fadeObject);
    }

    // Normalize returns a normalized (2D) vector which is where the co-ordinates summed together equals 1.
    private Vector2 Normalize(Vector2 vector)
    {
        // Set the new x and y value to the original x/y divided by the total distance of the vector.
        float newXValue = vector.x / VectorMagnitude(vector);
        float newYValue = vector.y / VectorMagnitude(vector);

        // Constructs the new normalized vector and returns it.
        Vector2 normalizedVector = new Vector2(newXValue, newYValue);
        return normalizedVector;
    }

    // A function that returns the magnitude of a vector.
    private float VectorMagnitude(Vector2 vector)
    {
        // Pythagorus a² + b² = c², calculates a² + b², then calculates the √(a² + b²)
        float pythagoriumSum = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2);
        float squareRoot = Mathf.Sqrt(pythagoriumSum);

        // Returns the magnitude/length of the vector.
        return squareRoot;
    }

    // A function that returns if a Vector is normal, which means its values aren't equal to NaN, Infinity, etc.
    private bool VectorIsNormal(Vector2 vector)
    {
        // Sets bools for if x and y are normal.
        bool xIsNormal = float.IsNormal(vector.x);
        bool yIsNormal = float.IsNormal(vector.y);

        // Returns if both x and y are normal.
        return xIsNormal && yIsNormal;
    }
}

// A collection object for the win/lose UI.
public struct UIAudioCollection
{
    // The background gradient panel.
    public GameObject panel;

    // The level number digits.
    public GameObject digits;

    // The music object.
    public GameObject music;


    // The collections of UI to batch fade.
    public FadeUICollection uiFadeCollection;

    // The number handler for the digits.
    public NumberHandler digitsNumberHandler;

    // The music fader.
    public FadeAudio musicFade;

    #nullable enable
    public UIAudioCollection(string panelPath, string digitsPath, string audioPath)
    {
        // Set everything to its corresponding parameter / get the child component.
        this.panel = GameObject.Find(panelPath);
        this.uiFadeCollection = this.panel.GetComponent<FadeUICollection>();
        this.digits = GameObject.Find(digitsPath);
        this.digitsNumberHandler = this.digits.GetComponent<NumberHandler>();
        this.music = GameObject.Find(audioPath);
        this.musicFade = this.music.GetComponent<FadeAudio>();
    }
}
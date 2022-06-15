using UnityEngine;
using UnityEngine.UI;

public class TrashMove : MonoBehaviour
{
    // This piece of trash's speed.
    private float thisTrashSpeed;

    // Stores the screen width of the last frame.
    private float previousScreenWidth;

    // Start is called before the first frame update.
    private void Start()
    {
        // Set the speed to a speed within the range of speeds.
        thisTrashSpeed = Random.Range(GameState.minTrashSpeed, GameState.maxTrashSpeed);
    }

    // Update is a method called once per frame.
    private void Update()
    {
        // Changes this objects position by the speed (downwards).
        Vector3 velocity = new Vector3();
        velocity.y = thisTrashSpeed * Time.deltaTime;
        gameObject.transform.position -= velocity;

        // If the screen width is different since last frame
        if (previousScreenWidth != Screen.width)
        {
            float leftOfScreen   = Camera.main.ScreenToWorldPoint(Vector3.right * 0).x;
            float rightOfScreen  = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x;

            // Check if the trash is off the screen
            if (transform.position.x < leftOfScreen || transform.position.y > rightOfScreen)
            {
                // Adjust its position to a random screen position.
                transform.position = new Vector3(ObjectSpawner.RandomPositionWithinScreenBounds().x, transform.position.y, 0);
            }
        }
        previousScreenWidth = Screen.width;
    }

    // Runs when this object collides with another object.
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.name)
        {
            // If the object collided with the trash killer.
            case "Trash Killer":
                // Decrements the player's lives.
                GameState.currentLives--;
                
                // Updates the health bar.
                GameObject healthUI = GameObject.Find("UI/Health");
                ImageHandler healthImage = healthUI.GetComponent<ImageHandler>();
                healthImage.NextSprite();

                // If the player has died then run the fail function.
                if (GameState.currentLives <= 0)
                    GameState.player.DoFail();
                
                break;

            // If the object collided with the player.
            case "Player":
                // Increment the score.
                GameState.score++;

                // Update the score UI.
                GameObject scoreUI = GameObject.Find("UI/Score/Digits");
                NumberHandler scoreText = scoreUI.GetComponent<NumberHandler>();
                scoreText.UpdateNumbers(GameState.score);

                // If the player has won, run the win function.
                if (GameState.score >= GameState.targetScore) 
                    GameState.player.DoWin();
                
                break;

            // Else, skip the rest of the code.
            default:
               return;
        }
        
        // Destroy this object.
        Destroy(gameObject);
    }
}

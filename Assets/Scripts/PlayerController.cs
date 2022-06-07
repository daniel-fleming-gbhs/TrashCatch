using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float MouseSpeed = 1000;
    public float KeyboardSpeed = 1250;
    public float maxSpeed = 2.5f;
    private Vector3 target;
    private Vector2 difference, force;
    public GameObject sprite;

    private GameObject WinScreen;
    private GameObject FailScreen;

    public bool gameLoopActive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        GameState.player = this;

        WinScreen = GameObject.Find("UI/Win");
        WinScreen.SetActive(false);
        FailScreen = GameObject.Find("UI/Fail");
        FailScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameLoopActive) return;

        // Add key and controller inputs
        rb2d.AddForce(Input.GetAxis("Horizontal") * KeyboardSpeed * Vector2.right);
        rb2d.AddForce(Input.GetAxis("Vertical") * KeyboardSpeed * Vector2.up);

        // if the left mouse button is being pressed
        if (Input.GetMouseButton(0)) {
            // get the mouse cursor position in world coordinates
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            // discard the z variables and calculate the distance between the character and player
            difference = new Vector2(transform.position.x, transform.position.y) - new Vector2(target.x, target.y);
            
            // if the distance is not NaN
            if (!(float.IsNaN(difference.x) || float.IsNaN(difference.y)))
            {
                // take the difference and normalise it so that the squares added together equal 1 (think triangles)
                force = -Normalize(difference); // flip the vector otherwise it is backwards
                
                // if the difference between the character and mouse is bigger than 0.25f
                // to avoid jitter
                if (VectorMagnitude(difference) > 0.25f)
                    rb2d.AddForce(force * Mathf.Clamp(VectorMagnitude(difference), 1, 3) * MouseSpeed); // scale the speed based on where the cursor is

                sprite.transform.rotation = Quaternion.Euler(force);
            }
        }

        // cap the velocity with the maxSpeed to avoid moving the character too fast
        rb2d.velocity = new Vector2(
            Mathf.Sign(rb2d.velocity.x) * Mathf.Min(Mathf.Abs(rb2d.velocity.x), maxSpeed), // get the minimum of the max speed vs the velocity while r
            Mathf.Sign(rb2d.velocity.y) * Mathf.Min(Mathf.Abs(rb2d.velocity.y), maxSpeed)
            );
    }

    public void fail() {
        GameObject.Find("Object Spawner").SetActive(false);
        gameLoopActive = false;

        FailScreen.SetActive(true);
    }

    public void win() {
        GameObject.Find("Object Spawner").SetActive(false);
        gameLoopActive = false;
        WinScreen.SetActive(true);
    }

    private Vector2 Normalize(Vector2 vector) {
        float x = vector.x / VectorMagnitude(vector);
        float y = vector.y / VectorMagnitude(vector);
        Vector2 NormalizedVector = new Vector2(x, y);
        return NormalizedVector;
    }
    private float VectorMagnitude(Vector2 vector) {
        float pythagorasSum = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2);
        float squareRoot = Mathf.Sqrt(pythagorasSum);
        return squareRoot;
    }
}

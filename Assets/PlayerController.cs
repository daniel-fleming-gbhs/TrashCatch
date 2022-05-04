using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 1000;
    public float maxSpeed = 2.5f;
    private Vector3 target;
    private Vector2 difference, force;

    public Text scoreDisplay;
    public int Score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score = value;
            Debug.Log(_Score);
            scoreDisplay.text = $"Score: {_Score}";
        }
    }
    private int _Score;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Add key and controller inputs
        rb2d.AddForce(Input.GetAxis("Horizontal") * speed * Vector2.right);
        rb2d.AddForce(Input.GetAxis("Vertical") * speed * Vector2.up);

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
                if (magnitude(difference) > 0.25f)
                    rb2d.AddForce(force * Mathf.Clamp(magnitude(difference), 1, 3) * speed); // scale the speed based on where the cursor is
            }
        }

        // cap the velocity with the maxSpeed to avoid moving the character too fast
        rb2d.velocity = new Vector2(
            Mathf.Sign(rb2d.velocity.x) * Mathf.Min(Mathf.Abs(rb2d.velocity.x), maxSpeed), // get the minimum of the max speed vs the velocity while r
            Mathf.Sign(rb2d.velocity.y) * Mathf.Min(Mathf.Abs(rb2d.velocity.y), maxSpeed)
            );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.name.Contains("Trash"))
            return;
        Destroy(collision.collider.gameObject);
        Score++;
    }

    private Vector2 Normalize(Vector2 vector) => new Vector2(vector.x / magnitude(vector), vector.y / magnitude(vector));
    private float magnitude(Vector2 vector) => Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
}

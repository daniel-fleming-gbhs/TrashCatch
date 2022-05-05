using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 1000;
    public float maxSpeed = 2.5f;

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

    private float magnitude(Vector2 vector) => Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
}

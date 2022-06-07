using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashMove : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(GameState.MinTrashSpeed, GameState.MaxTrashSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.name == "Trash Killer") {
            GameState.Lives--;
            GameObject.Find("UI/Lives").GetComponent<Text>().text = $"Lives: {GameState.Lives}";
            if (GameState.Lives == 0)
                GameState.player.fail();
        }
        else if (other.collider.name == "Player") {
            GameState.Score++;
            GameObject.Find("UI/Score").GetComponent<Text>().text = $"Score: {GameState.Score}";
            if (GameState.Score >= GameState.TargetScore)
                GameState.player.win();
        }
        else return;
        
        Destroy(gameObject);
    }
}

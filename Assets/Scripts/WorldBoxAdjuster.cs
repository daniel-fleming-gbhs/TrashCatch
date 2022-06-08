using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoxAdjuster : MonoBehaviour
{
    // The collider for the world bounding box.
    private EdgeCollider2D worldBoundingBoxCollider;

    // The collider for the trash box collider.
    private BoxCollider2D trashKillerBoxCollider;
    
    // Stores the screen width of the last frame.
    private float previousScreenWidth;

    // Start is called before the first frame update.
    private void Start()
    {
        // Gets the edge collider and trash killer box collider.
        worldBoundingBoxCollider = GetComponent<EdgeCollider2D>();
        trashKillerBoxCollider = GameObject.Find("Trash Killer").GetComponent<BoxCollider2D>();
    }
    
    // Update is a method called once per frame.
    private void Update()
    {
        // Adjust the box if the screen width changes.
        if (previousScreenWidth != Screen.width)
            AdjustBox();
        previousScreenWidth = Screen.width;
    }

    // Changes the size of the world bounding box and keeps the player inside.
    private void AdjustBox()
    {
        // The points of the screen.
        float leftOfScreen   = Camera.main.ScreenToWorldPoint(Vector3.right * 0).x;
        float rightOfScreen  = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x;
        float topOfScreen    = Camera.main.ScreenToWorldPoint(Vector3.up * Screen.height).y;
        float bottomOfScreen = Camera.main.ScreenToWorldPoint(Vector3.down * 0).y;

        // The new points of the collider.
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(leftOfScreen, topOfScreen);
        points[1] = new Vector2(rightOfScreen, topOfScreen);
        points[2] = new Vector2(rightOfScreen, bottomOfScreen);
        points[3] = new Vector2(leftOfScreen, bottomOfScreen);
        points[4] = points[0];

        // Adjust the collider.
        worldBoundingBoxCollider.points = points;

        // If the player is outside the collider horizontally, move the player to the centre of the screen horizontally.
        if (GameState.player.transform.position.x > rightOfScreen || GameState.player.transform.position.x < leftOfScreen)
        {
            GameState.player.transform.position = Vector3.up * GameState.player.transform.position.y;
        }

        // If the player is outside the collider vertically, move the player to the centre of the screen vertically.
        if (GameState.player.transform.position.y < bottomOfScreen || GameState.player.transform.position.y > topOfScreen)
        {
            GameState.player.transform.position = Vector3.right * GameState.player.transform.position.x;
        }

        // Adjust the trash killer box collider to the horizontal size.
        trashKillerBoxCollider.size = new Vector3(2 * rightOfScreen + 1, 0.25f, 0);
    }
}

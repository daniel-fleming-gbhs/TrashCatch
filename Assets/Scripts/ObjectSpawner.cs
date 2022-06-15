using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // The array of potential prefabs.
    public GameObject[] possiblePrefabs;

    // The time between object spawn ticks (seconds).
    public float timeBetweenObjectSpawnTicks;

    // The delay before trash starts spawning (seconds).
    public float startDelay;

    // The number of pieces of trash spawned for naming purposes.
    private int amountOfTrashSpawned;

    // Start is called before the first frame update.
    private void Start()
    {
        // Start a repeating call to SpawnObject() after {startDelay} seconds every {timeBetweenObjectSpawnTicks}. 
        InvokeRepeating("SpawnObject", startDelay, timeBetweenObjectSpawnTicks);
    }

    // Spawns an object.
    public void SpawnObject()
    {
        // If the game isn't running, do not spawn anymore objects.
        if (!GameState.gameLoopActive)
            return;
        
        // If the spawn chance fails skip the rest of the code.
        if (Random.Range(0, 1) > GameState.trashSpawnChance && !GameState.isInMenu)
            return;
        
        // Generates a random index of the prefabs array, gets a copy of the prefab.
        int randomPrefabIndex = (int)Random.Range(0, possiblePrefabs.Length);
        GameObject randomPrefab = possiblePrefabs[randomPrefabIndex];

        // Calculates a random position that is within the Camera's view.
        Vector3 randomPosition = RandomPositionWithinScreenBounds();

        // Creates the new object with the position and prefab with this as the parent.
        GameObject newObject = Instantiate(randomPrefab, randomPosition, Quaternion.Euler(Vector3.zero), this.transform);

        // Increment the amount of trash spawned and rename the new object to the number.
        amountOfTrashSpawned++;
        newObject.name = $"Trash {amountOfTrashSpawned}";
    }

    // Generates a random position within horizontal bounds and above the view area.
    public static Vector3 RandomPositionWithinScreenBounds()
    {
        // The position of the corners. 
        float leftOfScreen = Camera.main.ScreenToWorldPoint(Vector3.right * 0).x;
        float rightOfScreen = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x;
        float topOfScreen = Camera.main.ScreenToWorldPoint(Vector3.up * Screen.height).y;

        // A random position within the corners* (* less than the edges) and returns it.
        float randomXPosition = Random.Range(leftOfScreen * 0.9f, rightOfScreen * 0.9f);
        float randomYPosition = Random.Range(topOfScreen + 1, topOfScreen + 3);
        return new Vector3(randomXPosition, randomYPosition, 0);
    }
}

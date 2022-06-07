using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float frequencyOfSpawns;
    public float repetitionTime;
    private int TrashSpawned;

    void Start() {
        InvokeRepeating("SpawnObject", 2, repetitionTime);
    }

    // Start is called before the first frame update
    void SpawnObject() {
        if (TrashSpawned >= GameState.TargetScore + (GameState.MaxLives - GameState.Lives) - 1)
            CancelInvoke("SpawnObject");
        
        if (Random.Range(0, 1) >= frequencyOfSpawns)
            return;
        
        GameObject objectToSpawn = prefabs[(int)Mathf.Floor(Random.Range(0, prefabs.Length - 1))];
        Vector3 position = new Vector3(Random.Range(-8.2f, 8.2f), 5, 0);
        GameObject newObject = Instantiate(objectToSpawn, position, Quaternion.Euler(Vector3.zero), this.transform);
        TrashSpawned++;
        newObject.name = $"Trash {TrashSpawned}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashSpawner : MonoBehaviour
{
    public GameObject[] trashTemplates;
    public int trashAmount;

    // Start is called before the first frame update
    void Start()
    {
        if (trashTemplates.Length == 0) return;
        for (int i = 0; i < trashAmount; i++)
        {
            GameObject newGO = Instantiate(trashTemplates[(int)Mathf.Floor(Random.Range(0, trashTemplates.Length - 1))]);
            newGO.transform.position = new Vector3(Random.Range(-8.2f, 8.2f), Random.Range(4.9f, 50f), 0);
            newGO.SetActive(true);
            newGO.name = $"Trash {i + 1}";
        }
    }
}

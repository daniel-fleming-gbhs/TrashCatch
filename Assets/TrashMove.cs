using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMove : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.ToLower() == "seed")
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position += Vector3.down * speed;
    }
}

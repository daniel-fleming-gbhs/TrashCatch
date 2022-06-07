using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    private Image imageToChange;
    public Sprite[] sprites;
    private int currentSprite = 0;

    // Start is called before the first frame update
    void Start()
    {
        imageToChange = gameObject.GetComponent<Image>();
        if (sprites.Length > currentSprite)
            imageToChange.sprite = sprites[currentSprite];
    }

    // Update is called once per frame
    public void NextSprite()
    {
        if (sprites.Length == 0) return;
        currentSprite++;
        currentSprite = currentSprite % sprites.Length;
        imageToChange.sprite = sprites[currentSprite];
    }
}

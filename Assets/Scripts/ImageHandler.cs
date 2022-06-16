using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    // The image component that will be changed.
    private Image imageToChange;

    // The sprites that will be iterated through in NextSprite().
    public Sprite[] framesOfSprite;

    // The index of the currently shown sprite.
    private int currentActiveSprite = 0;

    // The Start method is called before the first frame update.
    private void Start()
    {
        // Get the image component.
        imageToChange = gameObject.GetComponent<Image>();

        try
        {
            // Set the sprite to the first of the array (because of the default value of currentActiveSprite).
            imageToChange.sprite = framesOfSprite[currentActiveSprite];
        }
        catch (IndexOutOfRangeException e)
        {
            // Print an error to the console for debugging and handling purposes.
            Debug.LogError($"Sprite[] sprites does not have any elements!\n{e}");
        }
    }

    // Increment the current sprite then update the image.
    public void NextSprite()
    {
        imageToChange = gameObject.GetComponent<Image>();

        try
        {   
            // Increment the currentActiveSprite and ensure it is within the range of the array's length.
            currentActiveSprite++;
            currentActiveSprite = currentActiveSprite % framesOfSprite.Length;
            
            // Update the sprite.
            imageToChange.sprite = framesOfSprite[currentActiveSprite];
        }
        catch (IndexOutOfRangeException e)
        {
            // Print an error to the console for debugging and handling purposes.
            Debug.LogError($"Sprite[] framesOfSprite does not have any elements!\n{e}");
        }
    }

    // Sets the current sprite to a given number.
    public void SetSpriteTo(int spriteNumber)
    {
        imageToChange = gameObject.GetComponent<Image>();
        
        try
        {
            // Update the current Active Sprite to the new Sprite Number.
            currentActiveSprite = spriteNumber;

            // Update the sprite.
            imageToChange.sprite = framesOfSprite[currentActiveSprite];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"Sprite[] framesOfSprite does not have {spriteNumber} elements!\n{e}");

            if (framesOfSprite.Length > 0)
            {
                // Sets the currentActiveSprite number to a number that is within the range of the length of array of sprites.
                currentActiveSprite = currentActiveSprite % framesOfSprite.Length;

                // Then re-runs the function with the new number that is definitely in bounds.
                SetSpriteTo(currentActiveSprite);
            }
        }
    }
}

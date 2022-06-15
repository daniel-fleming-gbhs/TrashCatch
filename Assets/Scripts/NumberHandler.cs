using System;
using UnityEngine;

public class NumberHandler : MonoBehaviour
{
    public ImageHandler[] digits;

    public void UpdateNumbers(int score)
    {
        try
        {
            ImageHandler.UpdateSpriteFont(score, digits);
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"There are no elements in the digits[]!\n{e}");
        }
    }
}

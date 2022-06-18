using System;
using UnityEngine;

public class NumberHandler : MonoBehaviour
{
    // The digits to change.
    public ImageHandler[] digits;

    // Updates the individual images according to the number given.
    public void UpdateNumbers(int numberToSetTo)
    {
        try
        {
            for (int powersOfTen = 0; powersOfTen < digits.Length; powersOfTen++)
            {
                // Divides the number by 10 to the power of {powersOfTen} which cuts of the lower numbers
                int digitAtPower = (int)(numberToSetTo / Math.Pow(10, powersOfTen));

                // Used mod 10 to remove any number greater than the ones place value.
                digitAtPower = digitAtPower % 10;

                // Update the sprite.
                digits[powersOfTen].SetSpriteTo(digitAtPower);
            }
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"There are no elements in the digits[]!\n{e}");
        }
    }
}

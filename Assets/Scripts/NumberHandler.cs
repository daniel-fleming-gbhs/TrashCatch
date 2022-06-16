using System;
using UnityEngine;

public class NumberHandler : MonoBehaviour
{
    public ImageHandler[] digits;

    public void UpdateNumbers(int numberToSetTo)
    {
        try
        {
            for (int powersOfTen = 0; powersOfTen < digits.Length; powersOfTen++)
            {
                int digitAtPower = (int)(numberToSetTo / Math.Pow(10, powersOfTen));
                digitAtPower = digitAtPower % 10;

                digits[powersOfTen].SetSpriteTo(digitAtPower);
            }
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"There are no elements in the digits[]!\n{e}");
        }
    }
}

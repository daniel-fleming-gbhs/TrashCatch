using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUICollection : MonoBehaviour
{
    // The array of UI Components to fade.
    public FadeImage[] UIToFade;
    
    // Fades in the images.
    public void FadeIn()
    {
        // Iterate through all the UI Compenents to fade and fade them.
        for (int i = 0; i < UIToFade.Length; i++)
        {
            UIToFade[i].FadeInImage(); 
        }
    }

    // Fades out the images.
    public void FadeOut()
    {
        // Iterate through all the UI Compenents to fade and fade them.
        for (int i = 0; i < UIToFade.Length; i++)
        {
            UIToFade[i].FadeOutImage(); 
        }
    }
}

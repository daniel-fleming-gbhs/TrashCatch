using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUICollection : MonoBehaviour
{
    public Fade[] UIToFade;
    
    public void FadeTo()
    {
        for (int i = 0; i < UIToFade.Length; i++)
        {
            UIToFade[i].FadeTo(); 
        }
    }

    public void FadeFrom()
    {
        for (int i = 0; i < UIToFade.Length; i++)
        {
            UIToFade[i].FadeFrom(); 
        }
    }
}

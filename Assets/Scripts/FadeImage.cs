using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FadeImage : MonoBehaviour
{
    // How fast the fade happens.
    public float fadeSpeed;

    // The scene to fade to at the end of a fade in.
    public string sceneName;

    // If the script starts from a fade.
    public bool startFromFade;

    // Start is called before the first frame update
    void Start()
    {
        // Fade in the audio if it is meant to.
        if (startFromFade)
            FadeInImage();
    }

    // Public function that starts an image fade in.
    public void FadeInImage()
    {
        StartCoroutine(FadeInEnumeratorImage());
    }

    // Public function that starts an image fade out.
    public void FadeOutImage()
    {
        StartCoroutine(FadeOutEnumeratorImage());
    }

    // The Enumerator that fades in an image gradually.
    private IEnumerator FadeInEnumeratorImage()
    {
        Image image = gameObject.GetComponent<Image>();
        Color objectColor = image.color;
        float fadeAmount;
        
        // Increment the alpha while it is greater than 0.
        while (objectColor.a < 1)
        {
            fadeAmount = objectColor.a + ((1 / fadeSpeed) * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            image.color = objectColor;
            yield return null;
        }
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }

    // The Enumerator that fades out an image gradually.
    private IEnumerator FadeOutEnumeratorImage()
    {
        Image image = gameObject.GetComponent<Image>();
        Color objectColor = image.color;
        float fadeAmount;

        // Decrement the alpha while it is greater than 0.
        while (objectColor.a > 0)
        {
            fadeAmount = objectColor.a - ((1 / fadeSpeed) * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            image.color = objectColor;
            yield return null;
        }
    }
}

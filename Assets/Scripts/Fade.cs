using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Fade : MonoBehaviour
{

    public float fadeSpeed;
    public string sceneName;
    public bool startFromFade;

    // Start is called before the first frame update
    void Start()
    {
        if (startFromFade)
            FadeFrom();
    }

    public void SetFadeSpeed(float newFadeSpeed) {
        fadeSpeed = newFadeSpeed;
    }

    public void SetSceneName(string newSceneName) {
        sceneName = newSceneName;
    }

    public void FadeTo() {
        StartCoroutine(FadeInEnumeratorImage());
    }

    public void FadeFrom() {
        StartCoroutine(FadeOutEnumeratorImage());
    }

    public void FadeToText() {
        StartCoroutine(FadeInEnumeratorText());
    }

    public void FadeFromText() {
        StartCoroutine(FadeOutEnumeratorText());
    }

    private IEnumerator FadeInEnumeratorImage() {
        Color objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;
        
        while (gameObject.GetComponent<Image>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOutEnumeratorImage() {
        Color objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a > 0) {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }

    private IEnumerator FadeInEnumeratorText() {
        Color objectColor = gameObject.GetComponent<Text>().color;
        float fadeAmount;
        
        while (gameObject.GetComponent<Text>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Text>().color = objectColor;
            yield return null;
        }
    }

    private IEnumerator FadeOutEnumeratorText() {
        Color objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a > 0) {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }
}

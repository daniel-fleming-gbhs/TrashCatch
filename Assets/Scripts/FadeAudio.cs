using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    // How fast the fade happens.
    public float fadeSpeed;

    // If the script starts from a fade.
    public bool startFromFade;

    // Start is called before the first frame update
    void Start()
    {
        // Fade in the audio if it is meant to.
        if (startFromFade)
            FadeInAudio();
    }

    // Public function that starts an audio fade out.    
    public void FadeOutAudio()
    {
        StartCoroutine(FadeOutEnumerator());
    }

    // The Enumerator that fades out the audio gradually.
    private IEnumerator FadeOutEnumerator()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        float fadeAmount = audioSource.volume;
        
        // Decrement the volume while it is greater than 0.
        while (audioSource.volume > 0)
        {
            fadeAmount -= ((1 / fadeSpeed) * Time.deltaTime);
            audioSource.volume = fadeAmount;    
            yield return null;
        }

        audioSource.volume = 0;
    }

    // Public function that starts an audio fade in.
    public void FadeInAudio()
    {
        StartCoroutine(FadeInEnumerator());
    }

    // The Enumerator that fades in the audio gradually
    private IEnumerator FadeInEnumerator()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        float fadeAmount = audioSource.volume;

        // Increments the volume while it is less than 1
        while (audioSource.volume < 1)
        {
            fadeAmount += ((1 / fadeSpeed) * Time.deltaTime);
            audioSource.volume = fadeAmount;    
            yield return null;
        }

        audioSource.volume = 1;
    }
}

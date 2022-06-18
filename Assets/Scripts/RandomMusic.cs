using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    // The audio source to edit.
    private AudioSource thisAudioSource;

    // The songs to play
    public AudioClip[] Songs;

    // Start is called before the first frame update
    void Start()
    {
        // Get the audio source component.
        thisAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the music has finished playing, play the next random song.
        if (!thisAudioSource.isPlaying)
        {
            SelectRandomAudioClip(ref thisAudioSource, Songs);
            thisAudioSource.Play();
        }
    }

    // Selects a random audio clip from the songs array.
    public static AudioClip SelectRandomAudioClip(ref AudioSource audioSorce, AudioClip[] SongsArray)
    {
        int randomNumber = (int)Random.Range(0, SongsArray.Length);
        AudioClip randomAudioClip = SongsArray[randomNumber];
        audioSorce.clip = randomAudioClip;
        return randomAudioClip;
    }
}

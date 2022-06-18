using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour
{
    // Audio mixer to change the volume levels.
    public AudioMixer masterMixer;

    // The names of the slider objects.
    private string[] sliderNames = {"MasterVolume", "MusicVolume", "SFXVolume"};

    private void Start()
    {
        // Iterates through the sliders and updates them according to the volume value of the mixer.
        foreach (string slider in sliderNames)
        {
            GameObject sliderObject = GameObject.Find($"Settings UI/Sliders/{slider}");
            Slider sliderToChange = sliderObject.GetComponent<Slider>();

            float value;
            if (masterMixer.GetFloat(slider, out value))
                sliderToChange.value = value;
        }
    }

    // Sets the master volume level (used by sliders).
    public void SetMasterLevel(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    // Sets the music volume level (used by sliders).
    public void SetMusicLevel(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume);
    }

    // Sets the SFX volume level (used by sliders).
    public void SetSfxLevel(float volume)
    {
        masterMixer.SetFloat("SFXVolume", volume);
    }
}

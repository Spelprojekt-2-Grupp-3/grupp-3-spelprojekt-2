using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class SFXVolumeController : MonoBehaviour
{

    // This script is used to put on a slider to control the SFX volume of the whole game. This is connected to the sfxVCA in FMOD Studio.

    // Path to SFX VCA in FMOD
    [SerializeField]
    private string _sfxVCAName = "vca:/SFX";

    // Reference to slider
    [SerializeField]
    private Slider volumeSlider;

    private VCA _sfxVCA;

    private void Start()
    {
        // Fetch the VCA by name from FMOD
        _sfxVCA = RuntimeManager.GetVCA(_sfxVCAName);

        // If the VCA is valid then retrieve the saved volume setting from PlayerPrefs
        if (_sfxVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
            _sfxVCA.setVolume(initialVolume);
            if (volumeSlider != null)
            {
                // Set initial volume on the VCA
                volumeSlider.value = initialVolume;
                volumeSlider.onValueChanged.AddListener(SetVolume);
            }
        }
    }

    private void SetVolume(float volume)
    {
        // Again, checks if VCA is valid before attempting to change anything (the volume in this case)
        if (_sfxVCA.isValid())
        {
            // Set new volume on the VCA
            _sfxVCA.setVolume(volume);

            // Save the volume setting in PlayerPrefs so it persists
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class MusicVolumeController : MonoBehaviour
{

    // This script is used to put on a slider to control the music volume of the whole game. This is connected to the musicVCA in FMOD Studio.

    // Path to Music VCA in FMOD
    [SerializeField]
    private string _musicVCAName = "vca:/Music";

    // Reference to slider
    [SerializeField]
    private Slider volumeSlider;

    private VCA _musicVCA;

    private void Start()
    {
        // Fetch the VCA by name from FMOD
        _musicVCA = RuntimeManager.GetVCA(_musicVCAName);

        // If the VCA is valid then retrieve the saved volume setting from PlayerPrefs
        if (_musicVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
            _musicVCA.setVolume(initialVolume);
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
        if (_musicVCA.isValid())
        {
            // Set new volume on the VCA
            _musicVCA.setVolume(volume);

            // Save the volume setting in PlayerPrefs so it persists
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }
}
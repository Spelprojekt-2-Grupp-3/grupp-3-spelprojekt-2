using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class MasterVolumeController : MonoBehaviour
{

    // This script as a whole is used to put on a slider to control the master volume of the whole game. This is connected to the masterVCA in FMOD Studio.

    // Path to Master VCA in FMOD
    [SerializeField]
    private string _masterVCAName = "vca:/Master";
    
    // Reference to slider
    [SerializeField]
    private Slider volumeSlider;

    private VCA _masterVCA;

    private void Start()
    {
        // Fetch the VCA by name from FMOD
        _masterVCA = RuntimeManager.GetVCA(_masterVCAName);

        // If the VCA is valid then retrieve the saved volume setting from PlayerPrefs
        if (_masterVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
            _masterVCA.setVolume(initialVolume);
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
        if (_masterVCA.isValid())
        {
            // Set new volume on the VCA
            _masterVCA.setVolume(volume);

            // Save the volume setting in PlayerPrefs so it persists
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }
    }
}
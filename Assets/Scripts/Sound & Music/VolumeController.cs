using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class VolumeController : MonoBehaviour
{

    // This script as a whole is used to put on a slider to control the master volume of the whole game. This is connected to the masterVCA in FMOD Studio.

    // Path to Master VCA in FMOD
    [SerializeField]
    private string _masterVCAName = "vca:/Master";
    
    // Reference to slider
    [SerializeField]
    private Slider masterVolumeSlider;

    private VCA _masterVCA;
    
    // Path to Music VCA in FMOD
    [SerializeField]
    private string _musicVCAName = "vca:/Music";

    // Reference to slider
    [SerializeField]
    private Slider musicVolumeSlider;
    
    private VCA _musicVCA;
    
    // Path to SFX VCA in FMOD
    [SerializeField]
    private string _sfxVCAName = "vca:/SFX";

    // Reference to slider
    [SerializeField]
    private Slider sfxVolumeSlider;

    private VCA _sfxVCA;

    private void Awake()
    {
        Debug.Log("AWAKKKKEE");
        // Fetch the VCA by name from FMOD
        _masterVCA = RuntimeManager.GetVCA(_masterVCAName);
        _musicVCA = RuntimeManager.GetVCA(_musicVCAName);
        _sfxVCA = RuntimeManager.GetVCA(_sfxVCAName);

        // If the VCA is valid then retrieve the saved volume setting from PlayerPrefs
        if (_masterVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MasterVolume");
            //_masterVCA.setVolume(initialVolume);
            if (masterVolumeSlider != null)
            {
                // Set initial volume on the VCA
                masterVolumeSlider.value = initialVolume;
                masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            }
        }
        
        if (_musicVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MusicVolume");
            //_musicVCA.setVolume(initialVolume);
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = initialVolume;
                musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            }
        }
        
        if (_sfxVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("SFXVolume");
            //_sfxVCA.setVolume(initialVolume);
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = initialVolume;
                sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            }
        }
    }

    private void SetMasterVolume(float volume)
    {
        // Again, checks if VCA is valid before attempting to change anything (the volume in this case)
        if (_masterVCA.isValid())
        {
            _masterVCA.setVolume(volume);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }
    }
    
    private void SetMusicVolume(float volume)
    {
        if (_musicVCA.isValid())
        {
            _musicVCA.setVolume(volume);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }
    
    private void SetSFXVolume(float volume)
    {
        if (_sfxVCA.isValid())
        {
            _sfxVCA.setVolume(volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}
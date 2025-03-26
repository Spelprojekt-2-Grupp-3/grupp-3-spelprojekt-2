using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslandBoarding : MonoBehaviour
{
    [SerializeField] private bool starterIsland;
    [SerializeField] private GameObject playerCharacter;
    private PlayerInputActions playerControls;
    private InputAction board;
    [SerializeField, Tooltip("Location for where the player spawns when boarding")] private Transform playerBoardingLocation;
    [SerializeField] private GameObject boatCamera;
    [SerializeField] private GameObject playerCamera;
    private bool allowIslandBoard;
    private bool allowBoatBoard;
    public FMODUnity.EventReference islandTheme;
    public FMODUnity.EventReference ambiance;

    private MusicManager musicManager;

    private FMOD.Studio.EventInstance islandThemeInstance;
    private FMOD.Studio.EventInstance ambianceInstance;
    private FMOD.Studio.EventInstance musicOceanInstance;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        if (starterIsland)
        {
            islandThemeInstance.start();
        }
    }

    private void Awake()
    {
        islandThemeInstance = FMODUnity.RuntimeManager.CreateInstance(islandTheme);
        ambianceInstance = FMODUnity.RuntimeManager.CreateInstance(ambiance);
        
        var masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        var musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        var sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        if (masterVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
            masterVCA.setVolume(initialVolume);
        }

        if (musicVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
            musicVCA.setVolume(initialVolume);
        }
        
        if (sfxVCA.isValid())
        {
            float initialVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
            sfxVCA.setVolume(initialVolume);
        }
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        board = playerControls.Player.Interact;
        board.Enable();
        board.performed += TryBoarding;
    }

    private void OnDisable()
    {
        board.Disable();
        board.performed -= TryBoarding;
    }

    void Update()
    {
        islandThemeInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ambianceInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boat" && !playerCharacter.activeSelf)
        {
            allowIslandBoard = true;
        }

        if (other.gameObject.tag == "Player")
        {
            allowBoatBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boat")
        {
            allowIslandBoard = false;
        }

        if (other.gameObject.tag == "Player")
        {
            allowBoatBoard = false;
        }
    }

    private void TryBoarding(InputAction.CallbackContext context)
    {
        if (allowBoatBoard)
        {
            BoardBoat();
        }
        
        else if (allowIslandBoard)
        {
            BoardIsland();
        }
    }

    public void BoardIsland()
    {
        islandThemeInstance.start();
        ambianceInstance.start();
        Events.stopBoat?.Invoke();
        playerCharacter.transform.position = playerBoardingLocation.position;
        boatCamera.SetActive(false);
        playerCamera.SetActive(true);
        playerCharacter.SetActive(true);
        allowIslandBoard = false;
        allowBoatBoard = true;
        musicManager.StopOceanMusic();
    }

    public void BoardBoat()
    {
        islandThemeInstance.stop(0);
        ambianceInstance.stop(0);
        Events.startBoat?.Invoke();
        musicOceanInstance.start();
        playerCharacter.SetActive(false);
        playerCamera.SetActive(false);
        boatCamera.SetActive(true);
        allowBoatBoard = false;
        allowIslandBoard = true;
        musicManager.StartOceanMusic();
    }
}

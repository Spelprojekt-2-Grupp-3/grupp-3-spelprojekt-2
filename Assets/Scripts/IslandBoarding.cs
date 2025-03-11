using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslandBoarding : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    private PlayerInputActions playerControls;
    private InputAction board;
    [SerializeField, Tooltip("Location for where the player spawns when boarding")] private Transform playerBoardingLocation;
    [SerializeField] private bool isBoarded;
    [SerializeField] private GameObject boatCamera;
    [SerializeField] private GameObject playerCamera;
    private bool allowIslandBoard;
    private bool allowBoatBoard;
    public FMODUnity.EventReference islandTheme;

    private FMOD.Studio.EventInstance islandThemeInstance;

    private void Awake()
    {
        islandThemeInstance = FMODUnity.RuntimeManager.CreateInstance(islandTheme);
        playerControls = new PlayerInputActions();
        allowIslandBoard = false;
        allowBoatBoard = false;
    }

    private void OnEnable()
    {
        board = playerControls.Player.Interact;
    }

    private void OnDisable()
    {
        
    }

    void Update()
    {
        islandThemeInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        board.Enable();
        board.performed += TryBoarding;
        
        if (other.gameObject.tag == "Boat")
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
        board.Disable();
        board.performed -= TryBoarding;
        
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
        if (allowIslandBoard)
        {
            BoardIsland();
        }
        
        else if (allowBoatBoard)
        {
            BoardBoat();
        }
    }

    public void BoardIsland()
    {
        islandThemeInstance.start();
        Events.stopBoat?.Invoke();
        playerCharacter.transform.position = playerBoardingLocation.position;
        boatCamera.SetActive(false);
        playerCamera.SetActive(true);
        playerCharacter.SetActive(true);
        allowIslandBoard = false;
        allowBoatBoard = true;
    }

    public void BoardBoat()
    {
        islandThemeInstance.stop(0);
        Events.startBoat?.Invoke();
        playerCharacter.SetActive(false);
        playerCamera.SetActive(false);
        boatCamera.SetActive(true);
        allowBoatBoard = false;
        allowIslandBoard = true;
    }
}

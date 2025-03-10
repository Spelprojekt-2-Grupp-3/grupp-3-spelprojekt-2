using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslandBoarding : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInst;
    private PlayerInputActions playerControls;
    private InputAction board;
    [SerializeField, Tooltip("Location for where the player spawns when boarding")] private Transform playerBoardingLocation;
    [SerializeField] private bool isBoarded;
    [SerializeField] private GameObject boatCamera;
    private bool allowIslandBoard;
    private bool allowBoatBoard;

    private void Awake()
    {
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
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
            Events.stopBoat?.Invoke();
            playerInst = Instantiate(playerPrefab, playerBoardingLocation.position, playerPrefab.transform.rotation);
            boatCamera.SetActive(false);
        }
        
        
        if (allowBoatBoard)
        {
            if (playerInst is not null)
            {
                Destroy(playerInst);
            }
        }
        
    }
}

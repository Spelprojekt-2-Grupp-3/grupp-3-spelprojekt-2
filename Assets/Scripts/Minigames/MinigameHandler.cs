using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameHandler : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private InputAction interact;
    [SerializeField] private GameObject minigameCanvasPrefab;

    private bool allowStartMinigame;


    private void Awake()
    {
        playerInput = new PlayerInputActions();
        allowStartMinigame = false;
    }

    private void OnEnable()
    {
        interact = playerInput.Player.Interact;
        interact.Enable();
        interact.performed += InstantiateMinigame;
    }

    private void OnDisable()
    {
        interact.Disable();
        interact.performed -= InstantiateMinigame;
    }

    private void InstantiateMinigame(InputAction.CallbackContext context)
    {
        Instantiate(minigameCanvasPrefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        allowStartMinigame = true;
    }

    private void OnTriggerExit(Collider other)
    {
        allowStartMinigame = false;
    }
}

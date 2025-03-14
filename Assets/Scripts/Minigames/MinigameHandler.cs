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
    private bool hasBeenPlayed;
    private bool minigameQuestStart;
    [SerializeField] private int questID, step;

    private void Awake()
    {
        hasBeenPlayed = false;
        playerInput = new PlayerInputActions();
        allowStartMinigame = false;
        minigameQuestStart = false;
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
        if (!allowStartMinigame || hasBeenPlayed || !minigameQuestStart) return;
        Instantiate(minigameCanvasPrefab);
        hasBeenPlayed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        allowStartMinigame = true;
    }

    private void OnTriggerExit(Collider other)
    {
        allowStartMinigame = false;
    }

    public void MinigameQuestStart(int ID, int currentStep)
    {
        if (ID == questID && currentStep == step)
        {
            minigameQuestStart = true;
        }
    }
}

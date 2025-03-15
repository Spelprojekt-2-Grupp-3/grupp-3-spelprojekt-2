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
    private bool hasBeenInstantiated;
    private bool minigameQuestStart;
    private GameObject minigameInstance;
    [SerializeField] private int questID, step;

    private void Awake()
    {
        hasBeenInstantiated = false;
        playerInput = new PlayerInputActions();
        allowStartMinigame = false;
        minigameQuestStart = true;
    }

    private void OnEnable()
    {
        interact = playerInput.Player.Interact;
        interact.Enable();
        interact.performed += InstantiateMinigame;
        interact.performed += ReopenMinigame;
    }

    private void OnDisable()
    {
        interact.Disable();
        interact.performed -= InstantiateMinigame;
        interact.performed -= ReopenMinigame;
    }

    private void InstantiateMinigame(InputAction.CallbackContext context)
    {
        if (!allowStartMinigame || hasBeenInstantiated || !minigameQuestStart) return;
        minigameInstance = Instantiate(minigameCanvasPrefab);
        hasBeenInstantiated = true;
    }

    private void ReopenMinigame(InputAction.CallbackContext context)
    {
        if (hasBeenInstantiated && allowStartMinigame)
        {
            minigameInstance.SetActive(true);
        }
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

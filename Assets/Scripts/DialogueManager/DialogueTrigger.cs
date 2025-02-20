using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip("Drag the corresponding Ink JSON file for this NPC's dialogue. Each NPC should have a unique file.")]
    [SerializeField] private TextAsset inkJSON;
   
    // Checks if the player is close to NPC
    private bool playerInRange;
    private PlayerInputActions _playerInput;

    private void Awake()
    {
        playerInRange = false;
        _playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        // Check if player is inside the trigger and presses the "Interact" action
        if (playerInRange && _playerInput.Player.Interact.WasPressedThisFrame())
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
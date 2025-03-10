using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip("Drag the corresponding Ink JSON file for this NPC's dialogue. This one is the default/backup text")]
    [SerializeField] private TextAsset inkJSON;
    [Tooltip("Text file for the first interaction"), SerializeField] private TextAsset introTextJSON;
    [Tooltip("Text file for the quest Dialogue"), SerializeField] private TextAsset questTextJSON;
    [Tooltip("For if the character has an extra quest"), SerializeField] private TextAsset extraQuestJSON;

    [Header("Related Quest settings")] 
    [SerializeField] private QuestData questData;
    [SerializeField] private QuestData extraQuestData;
    
    private QuestLog questLog;
    
    private bool firstTalk;
   
    // Checks if the player is close to NPC
    private bool playerInRange;
    private PlayerInputActions _playerInput;

    private void Awake()
    {
        firstTalk = true;
        questLog = FindObjectOfType<QuestLog>();
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
            if (firstTalk && introTextJSON)
            {
                DialogueManager.GetInstance().EnterDialogueMode(introTextJSON);
                firstTalk = false;
            }
            else if(questData != null){
                if (questLog.TestForQuest(questData.questTitle, questData.questText) && questTextJSON)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(questTextJSON);
                }
            }             
            else if(inkJSON)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
            else if (extraQuestJSON && extraQuestData)
            {
                DialogueManager.GetInstance().EnterDialogueMode(extraQuestJSON);
            }
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
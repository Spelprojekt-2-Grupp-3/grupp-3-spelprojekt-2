using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip(
        "Drag the corresponding Ink JSON file for this NPC's dialogue. This one is the default/backup text"
    )]
    [SerializeField]
    private TextAsset fillerDialogueJSON;

    [Tooltip("Text file for the first interaction"), SerializeField]
    private TextAsset introductionDialogueJSON;

    [Tooltip("Text file for the quest Dialogue"), SerializeField]
    private TextAsset questTextJSON;

    [Tooltip("For if the character has an extra quest"), SerializeField]
    private TextAsset extraQuestJSON;

    [Header("Related Quest settings")] 
    [SerializeField] private List<int> questIDs;
    [SerializeField]private List<int> questSteps;

    private QuestLog questLog;
    private bool firstTalk;
    
    // Checks if the player is close to NPC
    private bool playerInRange;
    private PlayerInputActions _playerInput;

    private void Start()
    {
        firstTalk = true;
    }

    private void Awake()
    {
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
        Debug.Log("Started check");
        //We first make sure no dialogue is active
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        { 
            bool foundValidDialogue = false;
            //If it's the first time talking to someone, we'll play their introductory file
            if (firstTalk && introductionDialogueJSON)
            {
                DialogueManager.GetInstance().dialogueQueue.Add(introductionDialogueJSON);
                firstTalk = false;
                foundValidDialogue = true;
            }
            //Check to make sure there is a quest
            if (questTextJSON&& extraQuestJSON)
            {
                foreach (var ID in questIDs)
                {
                    foreach (var step in questSteps)
                    {
                        if(questLog.TestForQuest(ID,step))
                        {
                            if(!DialogueManager.GetInstance().dialogueQueue.Contains(questTextJSON))
                                DialogueManager.GetInstance().dialogueQueue.Add(questTextJSON);
                            
                            else if(!DialogueManager.GetInstance().dialogueQueue.Contains(extraQuestJSON))
                                DialogueManager.GetInstance().dialogueQueue.Add(extraQuestJSON);
                            
                            else
                                Debug.LogWarning("Found third valid quest",this);
                            foundValidDialogue = true;
                        }
                    }
                }
            }

            if (foundValidDialogue)
            {
                DialogueManager.GetInstance().EnterDialogueMode(DialogueManager.GetInstance().dialogueQueue[0]);
                DialogueManager.GetInstance().dialogueQueue.Remove(DialogueManager.GetInstance().dialogueQueue[0]);
            }
            //Else we default to filler dialogue, as long as there is a valid asset
            else if (fillerDialogueJSON)
            {
                DialogueManager.GetInstance().EnterDialogueMode(fillerDialogueJSON);
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

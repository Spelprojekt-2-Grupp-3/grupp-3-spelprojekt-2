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
    [SerializeField]
    public QuestData questData;

    [SerializeField]
    private QuestData extraQuestData;

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
            //If it's the first time talking to someone, we'll play their introductory file
            if (firstTalk && introductionDialogueJSON)
            {
                DialogueManager.GetInstance().EnterDialogueMode(introductionDialogueJSON);
                firstTalk = false;
            }
            //If it's not the first time, we first check to make sure there is a quest
            else if (questData != null)
            {
                //Checks if the quest log contains a quest with the appropriate title and description and returns true, else false
                if (
                    questLog.TestForQuest(questData.questTitle, questData.questText)
                    && questTextJSON
                )
                {
                    DialogueManager.GetInstance().EnterDialogueMode(questTextJSON);
                }
                else if (extraQuestJSON && extraQuestData)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(extraQuestJSON);
                }
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

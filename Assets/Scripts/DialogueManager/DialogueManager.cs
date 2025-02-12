using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    private Story _currentStory;
    
    //Readonly (idk why)
    public bool dialogueIsPlaying { get; private set; }
    
    private static DialogueManager _instance;
    private PlayerInputActions _playerInput;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in scene!");
        }
        _instance = this;
        
        dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        
        _playerInput = new PlayerInputActions();
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        //return if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        //Continue to the next line of the dialogue when click
        if (_playerInput.Player.Interact.WasPressedThisFrame()) 
        {
            ContinueStory();
        }
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
        
        //Reset the input buffer to prevent immediate skipping
        _playerInput.Player.Interact.Reset();
    }

    private IEnumerator ExitDialogueMode()
    {
        //Small delay before closing UI to avoid double clicks
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            dialogueText.text = _currentStory.Continue();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator layoutAnimator;
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] _choicesText;
    
    private Story _currentStory;
    
    //Readonly (I dont know why)
    public bool dialogueIsPlaying { get; private set; }
    
    private static DialogueManager _instance;
    private PlayerInputActions _playerInput;
    
    private const string SpeakerTag = "Speaker";

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in scene!");
        }
        _instance = this;
        
        dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        displayNameText = GameObject.Find("DisplayNameText").GetComponent<TextMeshProUGUI>();
        portraitAnimator = GameObject.Find("PortraitImage").GetComponent<Animator>();
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        
        //Get all the choices text
        _choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        
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
        if (_currentStory.currentChoices.Count == 0 && _playerInput.Player.Interact.WasPressedThisFrame()) 
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
        
        //Reset portrait, layout and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("Default");
        layoutAnimator.Play("NPC");
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
            //Set text for the current dialogeu line
            dialogueText.text = _currentStory.Continue();
            //Display the choices if this dialogue has any
            DisplayChoices();
            //Handle tags
            HandleTags(_currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            // Parse the tag
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed: " + tag);
                return; // Exit early if the tag is invalid
            }

            // Trim to remove any extra whitespace
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag
            if (tagKey == SpeakerTag)
            {
                // Determine layout animation based on speaker
                if (tagValue == "Cleo")
                {
                    layoutAnimator.Play("Player");
                }
                else
                {
                    layoutAnimator.Play("NPC");
                }
                
                // Determine the text and portrait based on speaker
                displayNameText.text = tagValue;
                portraitAnimator.Play(tagValue);
            }
            else
            {
                Debug.LogWarning("Tag came in but is not currently being handled: " + tagKey);
            }
        }
    }


    private void DisplayChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        //Check to make sure the UI can support the amount of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogWarning("More choices were given than the UI can support. Number of choices given: " 
                             + currentChoices.Count);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        //Event system requires we clear it first and then wait for atleast one frame before we set the current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}

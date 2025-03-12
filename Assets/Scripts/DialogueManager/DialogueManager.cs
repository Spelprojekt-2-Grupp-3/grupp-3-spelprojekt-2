using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnityResonance;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float typingSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject dialogueUI;

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI displayNameText;

    [SerializeField]
    private GameObject continueIcon;

    [SerializeField]
    private Animator portraitAnimator;

    [SerializeField]
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField]
    private GameObject choicesPanel;

    [SerializeField]
    private GameObject choicesParent;

    [SerializeField]
    private GameObject[] choices;

    //[SerializeField] private InventoryController inventoryController;

    private TextMeshProUGUI[] _choicesText;
    private Story _currentStory;

    //Readonly (I dont know why)
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager _instance;
    private PlayerInputActions _playerInput;
    private const string SpeakerTag = "Speaker";
    private Coroutine _displayLineCoroutine;
    private bool _canContinueToNextLine = false;
    private bool _canSkip = false;
    private bool _submitSkip = false;

    private QuestLog questLog;

    private InventoryController inventoryController;
    private InventoryMenu inventoryMenu;

    [Header("Package data list")]
    [SerializeField]
    private List<PackageData> packageDatas;

    private DialogueQuestUpdate InkQuestIntegrationUpdater;

    [HideInInspector]public List<TextAsset> dialogueQueue;
    
    
    
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in scene!");
        }
        _instance = this;
        
        dialogueUI = transform.Find("DialogueUI").gameObject;
        
        dialoguePanel = dialogueUI.transform.Find("DialoguePanel").gameObject;
        dialogueText = dialogueUI.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        displayNameText = dialogueUI.transform.Find("DisplayNameText").GetComponent<TextMeshProUGUI>();
        continueIcon = dialogueUI.transform.Find("ContinueIcon").gameObject;
        
        portraitAnimator = dialogueUI.transform.Find("PortraitFrame/PortraitImage").GetComponent<Animator>();
        layoutAnimator = dialogueUI.GetComponent<Animator>();
        
        choicesPanel = dialogueUI.transform.Find("ChoicesPanel").gameObject;
        choicesParent = dialogueUI.transform.Find("Choices").gameObject;
        
        if (choicesParent != null)
        {
            // Get all child objects of ChoicesPanel
            int childCount = choicesParent.transform.childCount;
            choices = new GameObject[childCount];
            _choicesText = new TextMeshProUGUI[childCount];

            for (int i = 0; i < childCount; i++)
            {
                choices[i] = choicesParent.transform.GetChild(i).gameObject;
                _choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        
        questLog = FindObjectOfType<QuestLog>();
        inventoryController = FindObjectOfType<InventoryController>();
        inventoryMenu = FindObjectOfType<InventoryMenu>();

        _playerInput = new PlayerInputActions();
        
        questLog.AddQuest();
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueUI.SetActive(false);
    }

    void BindExternal()
    {
        _currentStory.BindExternalFunction(
            "AddQuest",
            () =>
            {
                questLog.AddQuest();
            }
        );

        _currentStory.BindExternalFunction(
            "EditQuest",
            (int ID, int step) =>
            {
                questLog.UpdateQuest(ID, step);
            }
        );
    }

    private void Update()
    {
        if (_playerInput.Player.Interact.WasPressedThisFrame())
        {
            _submitSkip = true;
        }

        // Return if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // If player clicks and can continue, go to the next line
        if (_submitSkip && _canContinueToNextLine && _currentStory.currentChoices.Count == 0)
        {
            _submitSkip = false; // Reset input buffer
            ContinueStory();
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
       // _playerInput.Disable();
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueUI.SetActive(true);

        BindExternal();
        //
        // inventoryMenu.DisableControls();

        //Reset portrait, layout and speaker
        displayNameText.text = "Name";
        portraitAnimator.Play("Default");
        layoutAnimator.Play("NPC");
        //Doesnt work yet
        Events.stopPlayer?.Invoke();

        ContinueStory();

        //inventoryController.gameObject.SetActive(false);
    }

    private IEnumerator ExitDialogueMode()
    {
        // inventoryMenu.EnableControls();

        //Small delay before closing UI to avoid double clicks
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialogueUI.SetActive(false);
        dialogueText.text = "";
        //Doesnt work yet
        Events.startPlayer?.Invoke();
        MultipleDialogueStart();
    }

    private void MultipleDialogueStart()
    {
        if(dialogueQueue.Count==0) return;
        
        EnterDialogueMode(dialogueQueue[0]);
        dialogueQueue.Remove(dialogueQueue[0]);
    }
    
    
    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            //Set text for the current dialogue line
            if (_displayLineCoroutine != null)
            {
                StopCoroutine(_displayLineCoroutine);
            }
            _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));

            //Handle tags
            HandleTags(_currentStory.currentTags);
        }
        else
        {
            //inventoryController.gameObject.SetActive(false);
            StartCoroutine(ExitDialogueMode());
        }
    }

//ljudreferens
public EventReference dialogueSound; 
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = ""; // Clear previous text
        continueIcon.SetActive(false);
        HideChoices();

        _canContinueToNextLine = false;
        _submitSkip = false; // Reset input buffer

        StartCoroutine(CanSkip()); // Start skipping delay

        foreach (char letter in line.ToCharArray())
        {
            if (_canSkip && _submitSkip) // If player clicks AFTER the delay, show full text
            {
                _submitSkip = false;
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            //hopefully rätt???
            RuntimeManager.PlayOneShot(dialogueSound, Camera.main.transform.position);
            yield return new WaitForSeconds(typingSpeed);
        }

        continueIcon.SetActive(true);
        DisplayChoices();

        _canContinueToNextLine = true;
        _canSkip = false; // Reset skipping permission
    }

    private IEnumerator CanSkip()
    {
        _canSkip = false;
        yield return new WaitForSeconds(0.05f); // small delay to allow typewriter effect to start
        _canSkip = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
            choicesParent.SetActive(false);
            choicesPanel.SetActive(false);
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
            Debug.LogWarning(
                "More choices were given than the UI can support. Number of choices given: "
                    + currentChoices.Count
            );
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }

        // Enable the choices panel only if there are choices available
        if (currentChoices.Count > 0)
        {
            choicesParent.SetActive(true);
            choicesPanel.SetActive(true);
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
        Debug.Log(choiceIndex);
        if (_canContinueToNextLine)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }
}

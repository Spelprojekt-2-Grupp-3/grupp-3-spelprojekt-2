using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VeraMinigame : Minigames
{
    private PlayerInputActions playerControls;
    private InputAction submit, leftJoyStick, exit;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private CurrentInputIcons currentInput;
    [SerializeField] private GameObject water;
    [SerializeField, Range(0, 6)] private int shellAmount; 
    private GameObject canvasInstance;
    private GameObject currentlySelected;
    private List<GameObject> shells = new List<GameObject>();
    [SerializeField] private GameObject mouseMarker;
    private GameObject mouseMarkerInstance;
    [SerializeField] private Sprite cleanerShell, moreCleanShell, fullyCleanShell;
    [SerializeField] private EventReference cleanSound;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        questLog = FindObjectOfType<QuestLog>();
    }

    private void OnEnable()
    {
        submit = playerControls.UI.Submit;
        submit.Enable();
        leftJoyStick = playerControls.UI.Navigate;
        leftJoyStick.Enable();
        exit = playerControls.UI.ButtonEast;
        exit.Enable();
        exit.performed += CloseMinigame;
    }

    private void OnDisable()
    {
        submit.Disable();
        leftJoyStick.Disable();
        exit.Disable();
        exit.performed -= CloseMinigame;
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        Events.stopPlayer?.Invoke();
        canvasInstance = gameObject;
        var yPos = 0f;
        for (int i = 0; i < shellAmount; i++)
        {
            GameObject shell = Instantiate(shellPrefab, canvasInstance.transform);
            shell.transform.position = new Vector3(shell.transform.position.x, shell.transform.position.y + yPos);
            yPos += 105f;
            var shellComponent = shell.AddComponent<Shell>();
            shellComponent.dirtyLevel = 3;
            shells.Add(shell);
        }
        mouseMarkerInstance = Instantiate(mouseMarker, transform);

        EventSystem.current.SetSelectedGameObject(shells[0]);
    }
    
    public override void CloseMinigame(InputAction.CallbackContext context)
    {
        Events.startPlayer?.Invoke();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canvasInstance is null) return;
        if (submit.WasPerformedThisFrame())
        {
            if (currentlySelected is null)
            {
                var shell = EventSystem.current.currentSelectedGameObject.GetComponent<Shell>();
                if (shell is not null) // We only want to grab shells
                {
                    currentlySelected = shell.gameObject;
                    var pos = currentlySelected.transform.position;
                    pos.y += 5;
                    currentlySelected.transform.position = pos;
                }
            }
            else
            {
                if (EventSystem.current.currentSelectedGameObject.name == "Water")
                {
                    currentlySelected.GetComponent<Shell>().dirtyLevel -= 1;
                    RuntimeManager.PlayOneShot(cleanSound);
                    if (currentlySelected.GetComponent<Shell>().dirtyLevel == 2)
                    {
                        currentlySelected.GetComponent<Image>().sprite = cleanerShell;
                    }
                    if (currentlySelected.GetComponent<Shell>().dirtyLevel == 1)
                    {
                        currentlySelected.GetComponent<Image>().sprite = moreCleanShell;
                    }
                    if (currentlySelected.GetComponent<Shell>().dirtyLevel <= 0)
                    {
                        currentlySelected.GetComponent<Image>().sprite = fullyCleanShell;
                    }
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Bucket" && currentlySelected.GetComponent<Shell>().dirtyLevel <= 0)
                {
                    currentlySelected.SetActive(false);
                    shells.Remove(currentlySelected);
                    currentlySelected = null;
                    if (shells.Count <= 0)
                    {
                        StopMinigame();
                    }
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Bucket" &&
                         currentlySelected.GetComponent<Shell>().dirtyLevel > 0)
                {
                    Debug.Log("That object is not clean");
                }
            }
        }
        
        if (leftJoyStick.IsInProgress() && currentlySelected is not null)
        {
            var pos = currentlySelected.transform.position;
            currentlySelected.transform.position = pos;
        }

        if (currentlySelected is not null)
        {
            currentlySelected.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }

        if (EventSystem.current.currentSelectedGameObject is not null)
        {
            mouseMarkerInstance.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        questLog.UpdateQuest(ID,step);
        Destroy(canvasInstance);
        canvasInstance = null;
    }
    
    private void UpdateIcons()
    {
        if (mouseMarkerInstance is null || canvasInstance is null) return;
        mouseMarkerInstance.GetComponent<Image>().sprite = currentInput.currentInputDevice.interactSprite;
    }
}

public class Shell : MonoBehaviour
{
    [HideInInspector] public int dirtyLevel;
}

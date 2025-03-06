using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VeraMinigame : Minigames
{
    private PlayerInputActions playerControls;
    private InputAction submit, leftJoyStick;
    [SerializeField] private GameObject canvasPrefab;
    [SerializeField] private GameObject shellPrefab;

    [SerializeField, Range(0, 6)] private int shellAmount; 
    private GameObject canvasInstance;
    private GameObject shellInstance;
    private GameObject currentlySelected;
    private List<GameObject> shells = new List<GameObject>();

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        submit = playerControls.UI.Submit;
        submit.Enable();
        leftJoyStick = playerControls.Player.Move;
        leftJoyStick.Enable();
    }

    private void OnDisable()
    {
        submit.Disable();
        leftJoyStick.Disable();
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        canvasInstance = Instantiate(canvasPrefab);
        var yPos = 0f;
        for (int i = 0; i < shellAmount; i++)
        {
            var shell = Instantiate(shellPrefab, canvasInstance.transform);
            shell.transform.position = new Vector3(shell.transform.position.x, shell.transform.position.y + yPos);
            yPos += 105f;
            var shellComponent = shell.AddComponent<Shell>();
            shellComponent.dirtyLevel = 4;
            shells.Add(shell);
        }
        
        EventSystem.current.firstSelectedGameObject = shells[0];
    }

    private void Update()
    {
        if (submit.WasPerformedThisFrame())
        {
            if (currentlySelected is null)
            {
                var shell = EventSystem.current.currentSelectedGameObject.GetComponent<Shell>();
                if (shell is not null) // We only want to grab shells
                {
                    currentlySelected = shell.gameObject;
                    Debug.Log("Currentlyselected" + currentlySelected);
                    var pos = currentlySelected.transform.position;
                    pos.y += 5;
                    currentlySelected.transform.position = pos;
                }
            }
            else
            {
                if (EventSystem.current.currentSelectedGameObject.name == "Water")
                {
                    Debug.Log("scrub scrub");
                    
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
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
    }
}

public class Shell : MonoBehaviour
{
    [HideInInspector] public int dirtyLevel;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = System.Random;

public class UlrichMinigme : Minigames
{
    private List<Vector3> correctPositions;
    [SerializeField] private GameObject incorrectOrder;
    [SerializeField] private GameObject correctOrder;
    private PlayerInputActions playerControls;
    private InputAction submit, buttonWest;
    List<GameObject> objects = new List<GameObject>();
    [SerializeField] private CurrentInputIcons currentInput;
    private GameObject selectedObject;
    [SerializeField] private Image confirmIcon;
    [SerializeField] private GameObject hoverMarker;

    private void Start()
    {
        StartMinigame();
    }

    private void OnEnable()
    {
        submit = playerControls.UI.Submit;
        buttonWest = playerControls.UI.ButtonWest;
        submit.Enable();
        buttonWest.Enable();
        Events.checkInputEvent.AddListener(SelectionHandler);
        Events.updateIcons.AddListener(UpdateIcons);
    }

    private void OnDisable()
    {
        submit.Disable();
        buttonWest.Disable();
        Events.checkInputEvent.RemoveListener(SelectionHandler);
        Events.updateIcons.RemoveListener(UpdateIcons);
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    public override void StartMinigame()
    {
        Events.stopPlayer?.Invoke();
        incorrectOrder.name = "Incorrect Order";
        List<Vector3> shuffledList = new List<Vector3>();
        for (int i = 0; i < incorrectOrder.transform.childCount; i++)
        {
            shuffledList.Add(incorrectOrder.transform.GetChild(i).transform.position);
        }
        
        correctPositions = shuffledList;

        Random random = new Random();

        shuffledList = shuffledList.OrderBy(x => random.Next()).ToList();
        
        for (int i = 0; i < incorrectOrder.transform.childCount; i++)
        {
            objects.Add(incorrectOrder.transform.GetChild(i).transform.gameObject);
        }
        
        int j = 0;
        foreach (var child in shuffledList)
        {
            objects[j].transform.position = child;
            j++;
        }
        EventSystem.current.SetSelectedGameObject(objects[0]);
        UpdateIcons();
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null) return;
        if (submit.WasPerformedThisFrame())
        {
            if (selectedObject is null)
            {
                selectedObject = EventSystem.current.currentSelectedGameObject;
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x,
                    selectedObject.transform.position.y + 10, selectedObject.transform.position.z);
            }
            else
            {
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x,
                    selectedObject.transform.position.y - 10, selectedObject.transform.position.z);
                (EventSystem.current.currentSelectedGameObject.transform.position, selectedObject.transform.position) 
                    = (selectedObject.transform.position, EventSystem.current.currentSelectedGameObject.transform.position);
                selectedObject = null;
            }
        }

        hoverMarker.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;

        if (buttonWest.WasPerformedThisFrame())
        {
            for (int i = 0; i < correctOrder.transform.childCount; i++)
            {
                var name = correctOrder.transform.GetChild(i).name;

                
            
                if (Mathf.RoundToInt(incorrectOrder.transform.Find(name).transform.localPosition.x) !=
                    Mathf.RoundToInt(correctOrder.transform.Find(name).transform.localPosition.x))
                {
                    return;
                }
            }
            StopMinigame();
        }
    }

    private void UpdateIcons()
    {
        confirmIcon.sprite = currentInput.currentInputDevice.buttonWest;
        hoverMarker.GetComponent<Image>().sprite = currentInput.currentInputDevice.buttonSouth;
    }

    private void SelectionHandler(PlayerInput sentInputDevice)
    {
        if (sentInputDevice.currentControlScheme.ToLower().Contains("keyboard"))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(objects[0]);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = System.Random;

public class UlrichMinigme : Minigames
{
    private List<Vector3> correctPositions;
    private GameObject incorrectOrder;
    [SerializeField] private GameObject correctOrder;
    private PlayerInputActions playerControls;
    private InputAction submit;
    List<GameObject> objects = new List<GameObject>();
    [SerializeField] private CurrentInputIcons currentInput;
    private GameObject selectedObject;

    private void Start()
    {
        StartMinigame();
    }

    private void OnEnable()
    {
        submit = playerControls.UI.Submit;
        submit.Enable();
        Events.checkInputEvent.AddListener(SelectionHandler);
    }

    private void OnDisable()
    {
        submit.Disable();
        Events.checkInputEvent.RemoveListener(SelectionHandler);
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    public override void StartMinigame()
    {
        incorrectOrder = Instantiate(correctOrder, transform);
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
    }

    private void Update()
    {
        if (submit.WasPerformedThisFrame())
        {
            if (selectedObject is null)
            {
                selectedObject = EventSystem.current.currentSelectedGameObject;
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x,
                    selectedObject.transform.position.y, selectedObject.transform.position.z);
            }
            else
            {
                (EventSystem.current.currentSelectedGameObject.transform.position, selectedObject.transform.position) 
                    = (selectedObject.transform.position, EventSystem.current.currentSelectedGameObject.transform.position);
                selectedObject = null;
            }
        }
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

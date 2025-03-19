using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
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
    private InputAction submit, buttonWest, buttonEast;
    List<GameObject> objects = new List<GameObject>();
    private GameObject selectedObject;
    [SerializeField] private GameObject hoverMarker;
    [SerializeField] private EventReference potSound;

    private void Start()
    {
        StartMinigame();
    }

    private void OnEnable()
    {
        submit = playerControls.UI.Submit;
        buttonWest = playerControls.UI.ButtonWest;
        buttonEast = playerControls.UI.ButtonEast;
        submit.Enable();
        buttonWest.Enable();
        buttonEast.Enable();
        Events.checkInputEvent.AddListener(SelectionHandler);
        buttonEast.performed += CloseMinigame;
    }

    private void OnDisable()
    {
        submit.Disable();
        buttonWest.Disable();
        buttonEast.Disable();
        buttonEast.performed -= CloseMinigame;
        Events.checkInputEvent.RemoveListener(SelectionHandler);
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    public override void StartMinigame()
    {
        Events.stopPlayer?.Invoke();
        List<Vector3> shuffledList = new List<Vector3>();
        for (int i = 0; i < incorrectOrder.transform.childCount; i++)
        {
            shuffledList.Add(incorrectOrder.transform.GetChild(i).transform.position);
        }
        
        correctPositions = shuffledList;
        
        Random random = new Random();

        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int k = random.Next(i);
            
            (shuffledList[i], shuffledList[k]) = (shuffledList[k], shuffledList[i]);
        }
        
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
        hoverMarker.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
    
    public override void CloseMinigame(InputAction.CallbackContext context)
    {
        Events.startPlayer?.Invoke();
        gameObject.SetActive(false);
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null)
        {
            Debug.Log("null");
            return;
        }
        if (submit.WasPerformedThisFrame())
        {
            if (selectedObject is null)
            {
                selectedObject = EventSystem.current.currentSelectedGameObject;
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x,
                    selectedObject.transform.position.y + 10, selectedObject.transform.position.z);
                RuntimeManager.PlayOneShot(potSound);
            }
            else
            {
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x,
                    selectedObject.transform.position.y - 10, selectedObject.transform.position.z);
                (EventSystem.current.currentSelectedGameObject.transform.position, selectedObject.transform.position) 
                    = (selectedObject.transform.position, EventSystem.current.currentSelectedGameObject.transform.position);
                RuntimeManager.PlayOneShot(potSound);
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

    private void SelectionHandler(PlayerInput sentInputDevice)
    {
        if (objects.Count <= 0) return;
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

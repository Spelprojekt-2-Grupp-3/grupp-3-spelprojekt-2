using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SigridMinigame : Minigames
{
    private PlayerInputActions playerControls;
    private InputAction submit;
    [SerializeField] private CurrentInputIcons currentInput;
    private GameObject pickedObject;
    private EmptyFuse[] emptyFuses = new EmptyFuse[15];
    private Fuse[] fuses = new Fuse[9];
    private List<int> fusesPos = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
    [SerializeField] private GameObject hoverMarker;

    private void OnEnable()
    {
        Events.updateIcons.AddListener(UpdateIcons);
        submit = playerControls.UI.Submit;
        submit.Enable();
    }

    private void OnDisable()
    {
        Events.updateIcons.AddListener(UpdateIcons);
        submit.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        pickedObject = null;
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        Events.stopPlayer?.Invoke();
        EventSystem.current.firstSelectedGameObject = transform.Find("Fuse").gameObject;
        emptyFuses = gameObject.transform.Find("EmptyFuses").GetComponentsInChildren<EmptyFuse>();
        fuses = gameObject.transform.Find("Background").GetComponentsInChildren<Fuse>();
        for (int i = 0; i < 6; i++)
        {
            var random = fusesPos[Random.Range(0, fusesPos.Count)];
            var chosenFuse = emptyFuses[random];
            fusesPos.Remove(random);
            chosenFuse.emptyFuse = false;
        }

        foreach (int pos in fusesPos)
        {
            emptyFuses[pos].emptyFuse = true;
            emptyFuses[pos].GetComponent<Image>().color = Color.grey;
            emptyFuses[pos].voltage = Random.Range(0, 80f);
        }
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null) return;
        hoverMarker.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        if (pickedObject != null)
        {
            var selectedObjPos = EventSystem.current.currentSelectedGameObject.transform.position;
            pickedObject.transform.position = selectedObjPos;
            if (submit.WasPerformedThisFrame())
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<EmptyFuse>() && EventSystem.current.currentSelectedGameObject.GetComponent<EmptyFuse>().emptyFuse)
                {
                    pickedObject.GetComponent<Button>().enabled = false;
                    pickedObject = null;
                    EventSystem.current.currentSelectedGameObject.GetComponent<EmptyFuse>().emptyFuse = false;
                    int numberOfEmtpys = 0;
                    foreach (var emptyFuse in emptyFuses)
                    {
                        if (emptyFuse.emptyFuse)
                        {
                            numberOfEmtpys += 1;
                        }
                    }

                    if (numberOfEmtpys <= 0)
                    {
                        StopMinigame();
                    }
                }
            }
        }
    }

    public void PickUp(GameObject obj)
    {
        pickedObject = obj;
    }

    private void UpdateIcons()
    {
        hoverMarker.GetComponent<Image>().sprite = currentInput.currentInputDevice.interactSprite;
    }
}

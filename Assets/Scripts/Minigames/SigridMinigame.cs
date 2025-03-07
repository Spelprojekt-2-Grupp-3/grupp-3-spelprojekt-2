using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SigridMinigame : Minigames
{
    private GameObject pickedObject;

    private void Awake()
    {
        pickedObject = null;
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        EventSystem.current.firstSelectedGameObject = transform.Find("Background").transform.Find("Fuse").gameObject;
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null) return;
        if (pickedObject != null)
        {
            Debug.Log(pickedObject);
            var selectedObjPos = EventSystem.current.currentSelectedGameObject.transform.position;
            selectedObjPos.z = -0.1f;
            pickedObject.transform.position = selectedObjPos;
        }
    }

    public void PickUp(GameObject obj)
    {
        Debug.Log(obj);
        pickedObject = obj;
    }
}

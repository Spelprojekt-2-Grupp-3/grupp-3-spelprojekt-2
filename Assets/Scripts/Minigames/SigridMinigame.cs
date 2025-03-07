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

    public override void StartMinigame()
    {
        base.StartMinigame();
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

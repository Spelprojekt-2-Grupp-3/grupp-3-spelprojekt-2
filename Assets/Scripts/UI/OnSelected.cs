using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelected : MonoBehaviour, ISelectHandler
{
    public FMODUnity.EventReference selectSound;

    public void OnSelect (BaseEventData eventData) 
    {
        RuntimeManager.PlayOneShot(selectSound);
    }
}

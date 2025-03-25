using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AffectWorld : MonoBehaviour
{
    //First point light under lights in hierarchy
    [SerializeField, Tooltip("The gameobject for the lightsource that should be turned on")] GameObject lightSource;
    [SerializeField, Tooltip("What id and step should affect the world")] private int id, step;

    private void OnEnable()
    {
        Events.sendUpdatedQuest.AddListener(UpdateWorld);
    }

    private void OnDisable()
    {
        Events.sendUpdatedQuest.RemoveListener(UpdateWorld);
    }

    private void UpdateWorld(int questID, int questStep)
    {
        if (id == questID && questStep == step)
        {
            lightSource.SetActive(true);
        }
    }
}

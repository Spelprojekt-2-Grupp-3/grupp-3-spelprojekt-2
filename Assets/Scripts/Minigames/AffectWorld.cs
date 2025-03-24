using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AffectWorld : MonoBehaviour
{
    //First point light under lights in hierarchy
    [SerializeField] GameObject objektToAffectWorld;
    private Light intensity;
    //Multiplier should be 20000 for fyr
    [SerializeField] private float multiplier;
    //Length should be 100947 for fyr
    [SerializeField] private float intensityLength;

    private void Start()
    {
        intensity = objektToAffectWorld.GetComponent<Light>();
    }

    private void Update()
    {
        if (objektToAffectWorld.activeInHierarchy)
        {
            intensity.intensity = Mathf.PingPong(Time.time*multiplier, intensityLength);
        } 
    }
}

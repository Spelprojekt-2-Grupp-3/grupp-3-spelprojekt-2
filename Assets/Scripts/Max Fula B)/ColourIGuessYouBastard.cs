using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourIGuessYouBastard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // cunt = GetComponent<Renderer>();
        piss = GetComponent<ParticleSystem>();
    }

  
    private ParticleSystem piss;
    Color fuckyou = Color.red;

    // Renderer cunt;

    // Update is called once per frame
    void Update()
    {
        //Particle gradient sucks balls so we just interpolate the biatch
        fuckyou = Color.LerpUnclamped(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        //  cunt.material.color = fuckyou;

        //we reference the main module of the particle system and set the colour over lifetime module so the shader can read the colour values
        ParticleSystem.MainModule ma = piss.main;
        var cuntOVerLife = piss.colorOverLifetime;
        cuntOVerLife.color = fuckyou;
//        Debug.Log(fuckyou);
    }
}

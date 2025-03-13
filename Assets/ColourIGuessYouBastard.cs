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

    private ParticleSystem.Particle[] birdererer;
    private ParticleSystem piss;
    Color fuckyou = Color.red;

    // Renderer cunt;

    // Update is called once per frame
    void Update()
    {
        birdererer = new ParticleSystem.Particle[piss.particleCount];
        for (int i = 0; i < birdererer.Length; i++) {
            
         }
        fuckyou = Color.LerpUnclamped(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        //  cunt.material.color = fuckyou;

        ParticleSystem.MainModule ma = piss.main;
        var cuntOVerLife = piss.colorOverLifetime;
        cuntOVerLife.color = fuckyou;
        Debug.Log(fuckyou);
    }
}

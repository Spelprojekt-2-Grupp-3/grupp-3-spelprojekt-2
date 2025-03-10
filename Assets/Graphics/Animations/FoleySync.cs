using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;

public class FoleySync : MonoBehaviour 
{
    public EventReference footsteps;
    public void FootStep()
    {
        //Play Footstep
        RuntimeManager.PlayOneShot(footsteps, transform.position);
    }
}

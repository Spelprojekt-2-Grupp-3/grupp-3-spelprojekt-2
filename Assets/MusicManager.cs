using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicOceanInstance;
    private bool isPlaying = false;
    private bool isBengtPlaying = false;

    private void Start()
    {
        StartBengtMusic();
    }

    public void StartBengtMusic()
    {
        if (isBengtPlaying) return;

        musicOceanInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music_Islands/Music_Bengt");
        musicOceanInstance.start();
        isBengtPlaying = true;
    }

    public void StopBengtMusic()
    {
        if (!isBengtPlaying) return;

        musicOceanInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        isBengtPlaying = false;
    }

    public void StartOceanMusic()
    {
        if (isPlaying) return;

        musicOceanInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music_Ocean/Music_FullOcean");
        musicOceanInstance.start();
        isPlaying = true;
    }

    public void StopOceanMusic()
    {
        if (!isPlaying) return;

        musicOceanInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        isPlaying = false;
    }
}
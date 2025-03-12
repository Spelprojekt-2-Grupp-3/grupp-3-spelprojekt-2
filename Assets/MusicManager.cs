using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicOceanInstance;
    private bool isPlaying = false;

    void Start()
    {
        StartOceanMusic();
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
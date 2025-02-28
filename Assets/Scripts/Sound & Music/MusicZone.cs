using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicZone : MonoBehaviour
{
    // Reference the event in FMod which we want to play.
    [SerializeField] private EventReference musicEvent;
    private EventInstance musicInstance;

    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
    }



    // When entering the trigger zone, FadeOut is set to 0 and musicInstance starts. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicInstance.setParameterByName("FadeOut", 0);
            musicInstance.start();
        }
    }



    // When "Player" exits the trigger zone initiate FadeOut parameter by setting it to 1.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicInstance.setParameterByName("FadeOut", 1);
            StartCoroutine(ReleaseAfterDelay(1f));
        }
    }



    // A counter to measure when we want to relase the music instance.
    private IEnumerator ReleaseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        musicInstance.release();
        Debug.Log("Music Instance Released");
    }



    // Frees up resources when the game object is destroyed.
    private void OnDestroy()
    {
        musicInstance.release();
    }
}
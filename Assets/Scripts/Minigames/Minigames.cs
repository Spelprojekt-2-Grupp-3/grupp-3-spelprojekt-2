using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigames : MonoBehaviour
{
    [HideInInspector] public bool hasBeenPlayed;
    public virtual void StartMinigame()
    {
        // All minigames will have this function that starts the minigame
    }

    public virtual void StopMinigame()
    {
        // Stops the minigame
    }

    public virtual void CloseMinigame()
    {
        // Closes minigame
    }
}

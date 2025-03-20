using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Minigames : MonoBehaviour
{
    [HideInInspector] public bool hasBeenPlayed;
    [HideInInspector] public int ID, step;
    [HideInInspector] public QuestLog questLog;
    public virtual void StartMinigame()
    {
        // All minigames will have this function that starts the minigame
    }

    public virtual void StopMinigame()
    {
        // Stops the minigame
    }

    public virtual void CloseMinigame(InputAction.CallbackContext context)
    {
        // Closes minigame
    }
}

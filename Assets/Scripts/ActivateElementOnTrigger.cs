using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElementOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private bool playerInsideTriggerZone = false;

    private void Update()
    {
        if (!playerInsideTriggerZone) return;
        
        bool dialogueActive = DialogueManager.GetInstance().dialogueIsPlaying;
        bool cooldownActive = !DialogueManager.GetInstance().canStartNewDialogue;

        // If dialogue or cooldown is active, hide the icon
        if (dialogueActive || cooldownActive)
        {
            if (obj.GetComponent<Canvas>())
            {
                obj.GetComponent<Canvas>().enabled = false;
            }
            else
            {
                obj.SetActive(false);
            }
        } 
        else
        {
            if (obj.GetComponent<Canvas>())
            {
                obj.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInsideTriggerZone = true;
        
        if (obj.GetComponent<Canvas>())
        {
            obj.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInsideTriggerZone = false;
        
        if (obj.GetComponent<Canvas>())
        {
            obj.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            obj.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElementOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    private bool playerInsideTriggerZone = false;

    [SerializeField]
    private Material outlineMat;

    private void Update()
    {
        if (!playerInsideTriggerZone)
            return;

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
        outlineMat.SetFloat("_CutoffRange", 800);
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
         outlineMat.SetFloat("_CutoffRange", 0);
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

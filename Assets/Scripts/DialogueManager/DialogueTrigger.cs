using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IClick
{
   //Checks if the player is close to NPC
   private bool playerInRange;
   
   [Header("Ink JSON")]
   [SerializeField] private TextAsset inkJSON;

   private void Awake()
   {
      playerInRange = false;
   }

   public void OnClick()
   {
      //Starts dialogue if player is in range and no other dialogue is active
      if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
      {
         DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         playerInRange = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         playerInRange = false;
      }
   }
   
   
}

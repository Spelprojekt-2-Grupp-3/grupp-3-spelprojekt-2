using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IClick
{
   [Header("Dialogue Settings")]
   [Tooltip("Drag the corresponding Ink JSON file for this NPC's dialogue. Each NPC should have a unique file.")]
   [SerializeField] private TextAsset inkJSON;
   
   [Header("NPC Information")]
   [Tooltip("Name of the NPC. This is just for organization and does not affect the game.")]
   [SerializeField] private string npcName;
   
   //Checks if the player is close to NPC
   private bool playerInRange;

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

using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questBoxPrefab;

    [SerializeField]
    private GameObject questContainer;

    [SerializeField]
    private GameObject questMenuTextObject;
    private List<QuestData> questList = new List<QuestData>();
    private List<QuestData> finishedQuests = new List<QuestData>();
    private List<QuestData> toRemove = new List<QuestData>();

    public List<QuestData> questsData = new List<QuestData>();

    private List<Quest> questObjectList = new List<Quest>();

    public TextAsset congratulations;
    
    
    [SerializeField]
    private bool devving;
    private int siblingIndex;

    public void SetEnableState(bool state)
    {
        questContainer.SetActive(state);
        if (questMenuTextObject)
            questMenuTextObject.SetActive(state);
    }


    private void Update()
    {
        if (finishedQuests.Count == 4)
        {
            UpdateQuest(0,0);
            DialogueManager.GetInstance().EnterDialogueMode(congratulations);
        }
    }

    public void AddQuest()
    {
        foreach (var quest in questsData)
        {
            Debug.Log("Added quest");
            questList.Add(quest);
            quest.step = 0;
            var questVisual = Instantiate(questBoxPrefab).GetComponent<Quest>();
            questVisual.gameObject.transform.SetParent(questContainer.transform);
            questObjectList.Add(questVisual);
            questVisual.Set(quest);
        }
    }

    public void UpdateQuest(int questID, int step)
    {
        foreach (var quest in questList)
        {
            Debug.Log("looped questList");
            if (quest.ID == questID && quest.step == step)
            {
                Debug.Log("Updated quest with ID: "+ questID);
                if (!quest.NextStep())
                    CompleteQuest(quest);
                Debug.Log(quest.ID);
                foreach (var questObject in questObjectList)
                {
                    if(questID == questObject.ID)
                        questObject.UpdateText(quest);
                }
            }
        }
        RemoveQuests();
    }

    private void CompleteQuest(QuestData quest)
    {
        Debug.Log("Completed quest:" + quest.ID);
        finishedQuests.Add(quest);
        toRemove.Add(quest);
    }

    private void RemoveQuests()
    {
        if(toRemove==null) return;
        foreach (var quest in toRemove)
        {
            questList.Remove(quest);
        }
        toRemove.Clear();
    }

    public bool TestForQuest(int ID, int step)
    {
       bool toReturn = false;
       
       foreach (var quest in questList)
       {
           if (quest.ID == ID && quest.step == step)
           {
               toReturn = true;
           }
       }

       return toReturn;
    }

    public bool TestForCompletedQuest(string title, string description)
    {
        //foreach (var quest in finishedQuests)
        //{
        //    //if the quest log contains a quest matching name and description go ahead with quest dialogue
        //    if (quest.titleText.text == title && quest.infoText.text == description)
        //    {
        //        return true;
        //    }
        //}
//
        return false;
    }
}

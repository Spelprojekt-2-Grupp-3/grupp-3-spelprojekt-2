using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questBox;

    [SerializeField]
    private GameObject questContainer;

    [SerializeField]
    private GameObject questMenuTextObject;
    private List<QuestData> questList = new List<QuestData>();
    private List<QuestData> finishedQuests = new List<QuestData>();

    public List<QuestData> questsData = new List<QuestData>();

    [SerializeField]
    private bool devving;
    private int siblingIndex;

    private void Start()
    {
        //if (devving)
            //AddQuest("tempQuest", "tempquest");
    }

    public void SetEnableState(bool state)
    {
        questContainer.SetActive(state);
        if (questMenuTextObject)
            questMenuTextObject.SetActive(state);
    }
    
    public void AddQuest()
    {
        foreach (var quest in questsData)
        {
            Debug.Log("Added quest");
            questList.Add(quest);
        }
    }

    public void UpdateQuest(int questID)
    {
        foreach (var quest in questList)
        {
            if (quest.ID == questID)
            {
                quest.NextStep();
                Debug.Log(quest.ID);
            }
                
            

        }
    }

    public void CompleteQuest(int questIndex)
    {
       //if (questList.Contains(questList[questIndex]))
       //{
       //    siblingIndex--;
       //    questList[questIndex].transform.SetAsLastSibling();
       //    finishedQuests.Add(questList[questIndex]);
       //}
    }

    public void RemoveQuest(int questIndex)
    {
        //if (questList.Contains(questList[questIndex]))
        //{
        //    Destroy(questList[questIndex].gameObject);
        //    questList.Remove(questList[questIndex]);
//
        //    siblingIndex--;
        //}
        //else
        //    Debug.Log("Tried to remove a quest that wasn't in the quest-log");
    }

    public bool TestForQuest(string title, string description)
    {
       bool toReturn = false;
       //foreach (var quest in questList)
       //{
       //    if (quest.titleText.text == title && quest.infoText.text == description)
       //    {
       //        toReturn = true;
       //    }
       //}

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

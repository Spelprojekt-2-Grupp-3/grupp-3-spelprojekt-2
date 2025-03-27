using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questBoxPrefab;

    [SerializeField]
    private GameObject questContainer;

    [SerializeField]
    private GameObject questMenuTextObject;

    [SerializeField] private GameObject noticeObject;
    [SerializeField] private float popUpTimer;

    private float actualTimer;
    private bool popUpActive;
    
    private List<QuestData> questList = new List<QuestData>();
    private List<QuestData> finishedQuests = new List<QuestData>();
    private List<QuestData> toRemove = new List<QuestData>();

    public List<QuestData> questsData = new List<QuestData>();

    private List<Quest> questObjectList = new List<Quest>();

    [SerializeField] private List<GameObject> questObjects;

    [Tooltip("for fine adjustments of the placement of quests on menu"),SerializeField] private float offset;
    
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
        if (popUpActive)
        {
            actualTimer -= Time.deltaTime;
            if (actualTimer <= 0)
            {
                popUpActive = false;
                noticeObject.GetComponent<Animator>().SetTrigger("Out");
          //      noticeObject.SetActive(false);
            }
        }
    }

    public void AddQuest()
    {
        for (int i = 0; i < questsData.Count; i++)
        {
            questList.Add(questsData[i]);
            questsData[i].step = 0;

            var questVisual = questObjects[i].GetComponent<Quest>();
            
            questObjectList.Add(questVisual); //add it to list for text-updating later
            questVisual.SetInitial(questsData[i]); //set the initial variables for the quest
        }
    }

    public void UpdateQuest(int questID, int step)
    {
        foreach (var quest in questList)
        {
            if (quest.ID == questID && quest.step == step)
            {
                if (!quest.NextStep())
                    CompleteQuest(quest);
                foreach (var questObject in questObjectList)
                {
                    if(questID == questObject.ID)
                        questObject.UpdateText(quest);
                }

                popUpActive = true;
                noticeObject.GetComponent<Animator>().SetTrigger("In");
              //  noticeObject.SetActive(true);
                actualTimer = popUpTimer;
                noticeObject.GetComponentInChildren<TMP_Text>().text = quest.questTitle;
            }
        }
        //RemoveQuests();
    }

    private void CompleteQuest(QuestData quest)
    {
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

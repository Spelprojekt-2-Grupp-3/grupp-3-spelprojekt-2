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
    private List<Quest> questList = new List<Quest>();
    private List<Quest> finishedQuests = new List<Quest>();

    [SerializeField]
    private bool devving;
    private int siblingIndex;

    private void Start()
    {
        if (devving)
            AddQuest("tempQuest", "tempquest");
    }

    public void SetEnableState(bool state)
    {
        questContainer.SetActive(state);
        if (questMenuTextObject)
            questMenuTextObject.SetActive(state);
    }

    void AddQuest(QuestData data)
    {
        var prefab = Instantiate(questBox, questContainer.transform);

        var quest = prefab.GetComponent<Quest>();

        quest.Set(data);
        questList.Add(quest);
        prefab.transform.SetSiblingIndex(siblingIndex);

        siblingIndex++;
    }

    public void AddQuest(string questTitle, string questText)
    {
        // Debug.Log(questText);
        var prefab = Instantiate(questBox, questContainer.transform);

        var quest = prefab.GetComponent<Quest>();

        quest.Set(questText, questTitle);
        questList.Add(quest);
        prefab.transform.SetSiblingIndex(siblingIndex);

        siblingIndex++;
    }

    /// <summary>
    /// For updating a quest already on the list
    /// </summary>
    /// <param name="quest">The quest that is updated</param>
    /// <param name="updatedData">the new data, if any (just send old data if no new)</param>
    /// <param name="step">What step the quest should be on</param>
    void UpdateQuest(Quest quest, QuestData updatedData, int step = 0)
    {
        if (questList.Contains(quest))
        {
            quest.Set(updatedData, step);
        }
        else
        {
            Debug.LogWarning("Attempted to Update a quest that has not been added to quest-log");
        }
    }

    public void UpdateQuest(int questIndex, string Title, string info, int step = 0)
    {
        if (questList.Contains(questList[questIndex]))
        {
            questList[questIndex].Set(info, Title);
        }
        else
        {
            Debug.LogWarning("Attempted to Update a quest that has not been added to quest-log");
        }
    }

    void CompleteQuest(Quest quest, QuestData data = null)
    {
        if (questList.Contains(quest))
        {
            if (data != null)
                quest.data = data;
            siblingIndex--;
            quest.transform.SetAsLastSibling();
        }
    }

    public void CompleteQuest(int questIndex)
    {
        if (questList.Contains(questList[questIndex]))
        {
            siblingIndex--;
            questList[questIndex].transform.SetAsLastSibling();
            finishedQuests.Add(questList[questIndex]);
        }
    }

    public void RemoveQuest(int questIndex)
    {
        if (questList.Contains(questList[questIndex]))
        {
            Destroy(questList[questIndex].gameObject);
            questList.Remove(questList[questIndex]);

            siblingIndex--;
        }
        else
            Debug.Log("Tried to remove a quest that wasn't in the quest-log");
    }

    public bool TestForQuest(string title, string description)
    {
        bool toReturn = false;
        foreach (var quest in questList)
        {
            if (quest.titleText.text == title && quest.infoText.text == description)
            {
                toReturn = true;
            }
        }

        return toReturn;
    }

    public bool TestForCompletedQuest(string title, string description)
    {
        foreach (var quest in finishedQuests)
        {
            //if the quest log contains a quest matching name and description go ahead with quest dialogue
            if (quest.titleText.text == title && quest.infoText.text == description)
            {
                return true;
            }
        }

        return false;
    }
}

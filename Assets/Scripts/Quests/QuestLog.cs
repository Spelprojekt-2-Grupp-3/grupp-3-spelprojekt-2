using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private GameObject questContainer;
    private List<Quest> questList;


    void AddQuest(QuestData data)
    {
        var prefab = Instantiate(questBox, questContainer.transform);

        var quest = prefab.GetComponent<Quest>();
        
        quest.Set(data);
        questList.Add(quest);
    }

    /// <summary>
    /// For updating quests with more than one step
    /// </summary>
    /// <param name="quest">The quest that is updated</param>
    /// <param name="step">What step the quest should be on</param>
    /// <param name="updatedData">the new data, if any (just send old data if no new)</param>
    void UpdateQuest(Quest quest, int step, QuestData updatedData)
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

    void CompleteQuest(Quest quest, QuestData data)
    {
        
    }

    void RemoveQuest()
    {
        
    }
}

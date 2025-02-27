using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private GameObject questContainer;
    private List<Quest> questList;
    private List<Quest> finishedQuests;

    private int siblingIndex;
    void AddQuest(QuestData data)
    {
        var prefab = Instantiate(questBox, questContainer.transform);

        var quest = prefab.GetComponent<Quest>();
        
        quest.Set(data);
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

    void RemoveQuest(Quest toRemove)
    {
        if (questList.Contains(toRemove))
        {
            questList.Remove(toRemove);
            siblingIndex--;
        }
            
        else
            Debug.Log("Tried to remove a quest that wasn't in the quest-log");
    }
}

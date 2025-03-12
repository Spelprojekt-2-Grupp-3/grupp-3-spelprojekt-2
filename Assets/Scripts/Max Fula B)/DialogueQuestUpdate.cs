using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestUpdate : MonoBehaviour
{
    public Queue<QuestData> quests;

    void Start()
    {
        quests = new Queue<QuestData>();
    }

    public void SetQuest(string recipient, string questName, string questDescription)
    {
        GameObject g = GameObject.Find(recipient + "Dialogue");
        Debug.Log(g);
        //g.GetComponent<DialogueTrigger>().questData = new QuestData()
        //{
        //    questTitle = questName,
        //    questText = questDescription,
        //};
    }
}

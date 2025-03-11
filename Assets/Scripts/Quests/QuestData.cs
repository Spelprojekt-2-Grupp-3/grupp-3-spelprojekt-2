using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Scriptable Objects/Quest Data Object",
    order = 7,
    fileName = "New Quest Data"
)]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string questText;
    public string recipient;
    public int step;
    public int ID;

    public QuestTest quest = new QuestTest();

    public void NextStep()
    {
        if (step < quest.steps.Count)
        {
            step++;
            questTitle = quest.questName;
        }
    }
}

[System.Serializable]
public class QuestTest
{
    public string questName;
    public string questInfo;
    public List<string> steps = new List<string>();
}

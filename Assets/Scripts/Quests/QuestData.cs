using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(
    menuName = "Scriptable Objects/Quest Data Object",
    order = 7,
    fileName = "New Quest Data"
)]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string initialQuestText;
    public int step;
    public int ID;

    [HideInInspector]public string questText;
    
    //public QuestTest quest = new QuestTest();
    public List<string> steps = new List<string>();
    
    public bool NextStep()
    {
        bool successful = false;
        
        if (step <= steps.Count-1)
        {
            questText = steps[step];
            step++;
            successful = true;
        }

        return successful;
    }
}

[Serializable]
public class QuestTest
{
    //public List<string> steps = new List<string>();
}

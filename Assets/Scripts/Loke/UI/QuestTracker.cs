using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public string[] quests;
    
    private int questCount = 0;

    private string questName = "Quest";

    private string currQuest;

    private GameObject contentBox;

    [SerializeField] GameObject questBox;

    private new GameObject newQuest;
    
    public TMP_Text questText;

    // Start is called before the first frame update
    void Start()
    {
        quests = new string[10];
    }
// Update is called once per frame
    void Update()
    {
        while (questCount < 11)
        {
            currQuest = questName + " " + questCount;
            quests[questCount] = currQuest;
            questCount += 1;
        }
    }
}

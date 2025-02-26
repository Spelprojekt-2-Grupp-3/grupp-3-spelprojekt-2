using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text infoText;
    [HideInInspector] public QuestData data;

    public void Set(QuestData setData, int currentStep =0)
    {
        if(setData.steps == 0 && currentStep >0)
            Debug.Log("Attempted to progress to next step in step-less quest");
        if (setData.steps > 0)
        {
            infoText.text = currentStep + "/" + setData.steps + setData.questText;
            if (data != null && setData != data)
            {
                titleText.text = setData.questTitle;
            }
        }
        else if(data != null && setData != data)
        {
            titleText.text = setData.questTitle;
            infoText.text = setData.questText;
        }
        
        data = setData;
    }

    public void Set(string updatedInfo, string updatedTitle)
    {
        titleText.text = updatedTitle;
        infoText.text = updatedInfo;
    }

    void NextStep()
    {
        
    }
}

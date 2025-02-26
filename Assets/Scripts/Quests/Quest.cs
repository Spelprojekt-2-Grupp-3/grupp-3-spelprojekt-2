using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text infoText;

    public void Set(QuestData data, int currentStep =0)
    {
        if (data.steps > 0)
        {
            infoText.text = currentStep + "/" + data.steps + data.questText;
        }
        else
        {
            infoText.text = data.questText;
        }
        
        titleText.text = data.questTitle;
    }

    public void Set(string updatedInfo, string updatedTitle)
    {
        titleText.text = updatedTitle;
        infoText.text = updatedInfo;
    }

}

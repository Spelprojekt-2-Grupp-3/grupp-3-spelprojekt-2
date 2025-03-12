using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text infoText;
    [HideInInspector] public QuestData questData;
    
    public void Set()
    {
        
    }

    public void Set(string updatedInfo, string updatedTitle)
    {
        titleText.text = updatedTitle;
        infoText.text = updatedInfo;
    }
}

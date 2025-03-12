using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text infoText;
    [HideInInspector] public int ID;
    
    public void Set(QuestData data)
    {
        ID = data.ID;
        titleText.text = data.questTitle;
        infoText.text = data.questText;
    }

    public void UpdateText(QuestData data)
    {
        infoText.text = data.questText;
    }
}

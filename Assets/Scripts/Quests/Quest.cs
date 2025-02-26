using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public TMP_Text infoText;
    public TMP_Text titleText;

    public void Set(QuestData data)
    {
        titleText.text = data.questTitle;
        infoText.text = data.questText;
    }
    


}

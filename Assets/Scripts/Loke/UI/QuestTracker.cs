using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class QuestTracker : MonoBehaviour
{
    private string questName;

    [SerializeField] GameObject questBox;
    [SerializeField] GameObject questContainer;
    
    private string questText;

    //Räknar antalet nuvarande quests
    private int questIndex = 0;
    
    bool questToggle;

    // Start is called before the first frame update
    void Start()
    {
        //Exempel
        AddQuest("Talk to Bengt", "Bengt has not been talked to");
        AddQuest("Deliver Package to Ingrid", "0/1 Packages Delivered");
        AddQuest("Find the Kraken", "0/1 Kraken Found");
        RemoveQuest("Deliver Package to Ingrid", "1/1 package delivered");
        RemoveQuest("Talk to Bengt", "Bengt has been talked to");
        AddQuest("Do something fun", "Test");
        AddQuest("Do something evil", "Test");
        RemoveQuest("Do something evil", "You dropped grandma's stroller in the sea, she was very mad");
        AddQuest("Do something ugly", "Test");
    }

    void AddQuest(string questName, string questText)
    {
        //Instansierar en quest i quest log
        questBox = Instantiate(questBox,questContainer.transform);
        //Ändrar namnet på objektet, viktigt för att hitta det
        questBox.name = questName;
        //Hittar tmp_text komponent
        questBox.GetComponent<TMP_Text>().SetText(questName);
        //Ändrar texten på quest_text, reflekterar vilket stadie questen är i
        questBox.gameObject.transform.Find("Quest_Text").GetComponent<TMP_Text>().SetText(questText);
        //Sätter vart questen ska vara
        questBox.gameObject.transform.SetSiblingIndex(questIndex);
        //Ökar värdet på den variabeln
        questIndex++;
    }

    void RemoveQuest(string questName, string edit)
    {
        //Hittar questen som du letar efter
        GameObject questFinish = GameObject.Find(questName);
        questFinish.transform.Find("Quest_Text").GetComponent<TMP_Text>().SetText(edit);
        //Sätter den längst ner i hierarkin för quest log
        questFinish.transform.SetAsLastSibling();
        //Minskar värdet på variabeln
        questIndex--;
        /*//Hittar komponenten tmp_text
        TMP_Text edit = questFinish.GetComponent<TMP_Text>();
        //Stryker över texten
        edit.SetText("<s>" + questName + "</s>");
        edit.color = Color.black;*/
        questToggle = questFinish.GetComponentInChildren<Toggle>().isOn = true;
    }
}

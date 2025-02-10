using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UIElements.Toggle;

public class QuestTracker : MonoBehaviour
{
    private string questName;

    [SerializeField] GameObject questBox;
    [SerializeField] GameObject questContainer;
    
    private string questText;

    // Start is called before the first frame update
    void Start()
    {
        addQuest("Top", "Bottom");
        addQuest("Deliver Package to Ingrid", "0/1 Packages Delivered");
        addQuest("Find the Kraken", "0/1 Kraken Found");
        removeQuest("Deliver Package to Ingrid");
        removeQuest("Top");
    }

    void addQuest(string questName, string questText)
    {
        //Instansierar en quest i quest log
        questBox = Instantiate(questBox,questContainer.transform);
        //Ändrar namnet på objektet, viktigt för att hitta det
        questBox.name = questName;
        //Hittar tmp_text komponent
        questBox.GetComponent<TMP_Text>().SetText(questName);
        //Ändrar texten på quest_text, reflekterar vilket stadie questen är i
        questBox.gameObject.transform.Find("Quest_Text").GetComponent<TMP_Text>().SetText(questText);
    }

    void removeQuest(string questName)
    {
        //Hittar questen som du letar efter
        GameObject questFinish = GameObject.Find(questName);
        //Sätter den längst ner i hierarkin för quest log
        questFinish.transform.SetAsLastSibling();
        //Hittar komponenten tmp_text
        TMP_Text edit = questFinish.GetComponent<TMP_Text>();
        //Stryker över texten
        edit.SetText("<s>" + questName + "</s>");
    }
}

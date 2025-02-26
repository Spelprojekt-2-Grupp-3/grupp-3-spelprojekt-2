using TMPro;
using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;

public class QuestTracker : MonoBehaviour
{
    [SerializeField] GameObject questBox;
    [SerializeField] GameObject questContainer;

    //Räknar antalet nuvarande quests
    private int questIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Exempel på hur man använder
        AddQuest("Talk to Bengt", "Bengt has not been talked to");
        AddQuest("Deliver Package to Ingrid", "0/1 Packages Delivered");
        AddQuest("Find the Kraken", "0/1 Kraken Found");
        RemoveQuest("Deliver Package to Ingrid", "1/1 package delivered");
        RemoveQuest("Talk to Bengt", "Bengt has been talked to");
        AddQuest("Do something fun", "Test");
        AddQuest("Do something evil", "Test");
        RemoveQuest("Do something evil", "You dropped grandma's stroller in the sea, she was very mad");
        AddQuest("Do something ugly", "Test");
        UpdateQuest("Find the Kraken", "1/1 kraken found, talk to Bengt");
        //Ger info i console om det inte hittar
        UpdateQuest("Kill", "HEHE");
        RemoveQuest("Kill them", "Quest haha");
    }

    //Lägger till quest i quest log
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

    //Används för att uppdatera quest som har flera steg
    void UpdateQuest(string questName, string edit)
    {
        if (gameObject.transform.Find(questName) == null)
        {
            Debug.Log("Quest '"+ questName + "' was not found");
        }
        else
        {
            GameObject questFinish = GameObject.Find(questName);
            questFinish.transform.Find("Quest_Text").GetComponent<TMP_Text>().SetText(edit);
        }
    }

    //Avslutar quest i quest log
    void RemoveQuest(string questName, string edit)
    {
        if (gameObject.transform.Find(questName) == null)
        {
            Debug.Log("Quest '" + questName + "' was not found");
        }
        else
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
            bool questToggle = questFinish.GetComponentInChildren<Toggle>().isOn = true;
        }
    }
}
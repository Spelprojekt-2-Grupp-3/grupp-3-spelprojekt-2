using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    private Button infoButton;
    private Button button2;
    [SerializeField] private TMP_Text infoText;

    //private PackageData data;
    
    public void ShowInformation(PackageData data)
    {
        infoText.text = "Recipient: " + data.recipient;
    }
}

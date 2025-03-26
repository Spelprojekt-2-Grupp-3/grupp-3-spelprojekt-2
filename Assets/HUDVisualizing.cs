using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDVisualizing : MonoBehaviour
{
    [SerializeField]
    private GameObject controlScheme;

    [SerializeField]
    private TextMeshProUGUI myText;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (controlScheme.activeInHierarchy)
        {
            myText.text = "Hide HUD";
        }
        else
        {
            myText.text = "Show HUD";
        }
    }
}

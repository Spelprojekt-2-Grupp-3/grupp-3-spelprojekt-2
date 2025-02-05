using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private string s = "";

    public void Load(string s)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(s);
    }

    public void Exit()
    {
        Debug.Log("fucker");
        Application.Quit();
    }

    public void ToggleElement(GameObject g)
    {
        g.SetActive(!g.active);

        if (g.active)
            s = "Close";
        else
            s = "Credits";
    }

    public void ChangeTextElement(GameObject tmp)
    {
        tmp.GetComponent<TextMeshProUGUI>().text = s;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionTab : MonoBehaviour
{
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private GameObject tabVisuals;
    [SerializeField] private GameObject firstSelected;

    public void Click()
    {
        tabGroup.ButtonClicked(this);
    }

    public void EnableVisuals()
    {
        tabVisuals.SetActive(!tabVisuals.activeSelf);
        if (tabVisuals.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
    }

    public void DisableVisuals()
    {
        tabVisuals.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTab : MonoBehaviour
{
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private GameObject tabVisuals;

    public void Click()
    {
        tabGroup.ButtonClicked(this);
    }

    public void EnableVisuals()
    {
        tabVisuals.SetActive(!tabVisuals.activeSelf);
    }

    public void DisableVisuals()
    {
        tabVisuals.SetActive(false);
    }
}

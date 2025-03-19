using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<OptionTab> tabList = new List<OptionTab>();
    public void ButtonClicked(OptionTab optionTab)
    {
        foreach (var tab in tabList)
        {
            if (tab == optionTab)
            {
                tab.EnableVisuals();
            }
            else
            {
                tab.DisableVisuals();
            }
        }
    }
}

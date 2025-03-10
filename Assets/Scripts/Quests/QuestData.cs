using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Scriptable Objects/Quest Data Object",
    order = 7,
    fileName = "New Quest Data"
)]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string questText;
    public string recipient;
    public int steps;
}

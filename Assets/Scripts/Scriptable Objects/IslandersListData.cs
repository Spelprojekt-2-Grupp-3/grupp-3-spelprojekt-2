using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Islanders List",order = 0, fileName = "New Islanders List")]
public class IslandersListData : ScriptableObject
{
    public List<string> islanders;
}

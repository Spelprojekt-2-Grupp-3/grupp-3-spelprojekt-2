using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Kraken Difficulty")]

public class KrakenDifficulty : ScriptableObject
{
    [Range(0, 25)] public int krakenHP;
}

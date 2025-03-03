using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Kraken Difficulty")]

public class KrakenDifficulty : ScriptableObject
{
    [Range(0, 25)] public float hp;
    [Range(0, 10)] public float regenPerSecond;
}

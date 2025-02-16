using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenMinigame : Minigames
{
    [SerializeField] private GameObject kraken;
    [SerializeField] private KrakenDifficulty krakenDifficulty;

    public override void StartMinigame()
    {
        var krakenInstance = Instantiate(kraken);
        Events.stopBoat?.Invoke();
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }
}

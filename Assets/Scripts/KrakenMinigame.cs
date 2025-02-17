using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenMinigame : Minigames
{
    [SerializeField] private GameObject kraken;
    [SerializeField] private KrakenDifficulty krakenDifficulty;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject hpBar;

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        var krakenInstance = Instantiate(kraken);
        var canvasInst = Instantiate(canvas);
        var hpBarInst = Instantiate(hpBar, new Vector3(0, 0, 0), hpBar.transform.rotation, canvasInst.transform);
        Events.stopBoat?.Invoke();
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }
}

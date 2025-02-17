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
    private List<GameObject> hpBarInstances = new List<GameObject>();
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        StartMinigame();
    }

    public override void StartMinigame()
    {
        var canvasInst = Instantiate(canvas);
        canvasInst.GetComponent<Canvas>().worldCamera = camera;
        var krakenInstance = Instantiate(kraken, new Vector3(0, 0, 0), kraken.transform.rotation);
        Vector3 barPos = new Vector3(krakenInstance.transform.position.x, krakenInstance.transform.position.y + 15, krakenInstance.transform.position.z);
        var hpBarInst = Instantiate(hpBar, barPos, hpBar.transform.rotation, canvasInst.transform);
        hpBarInstances.Add(hpBarInst);
        Events.stopBoat?.Invoke();
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        foreach (var hpBarInst in hpBarInstances)
        {
            hpBarInst.transform.LookAt(camera.transform.position);
        }
    }
}

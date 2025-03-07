using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private GameObject minigameCanvasPrefab;

    private void Start()
    {
        InstantiateMinigame();
    }

    public void InstantiateMinigame()
    {
        Instantiate(minigameCanvasPrefab);
    }
}

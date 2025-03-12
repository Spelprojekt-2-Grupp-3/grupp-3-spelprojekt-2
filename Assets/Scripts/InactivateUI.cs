using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private void OnEnable()
    {
        UI.SetActive(false);
    }

    private void OnDisable()
    {
        UI.SetActive(true);
    }
}

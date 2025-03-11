using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElementOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        obj.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        obj.SetActive(false);
    }
}

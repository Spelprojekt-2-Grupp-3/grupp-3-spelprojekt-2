using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElementOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if (obj.GetComponent<Canvas>())
        {
            obj.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (obj.GetComponent<Canvas>())
        {
            obj.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            obj.SetActive(false);
        }
    }
}

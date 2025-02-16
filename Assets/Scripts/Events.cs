using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static UnityEvent exampleEvent;
    public static UnityEvent startBoat;
    public static UnityEvent stopBoat;

    private void OnEnable()
    {
        exampleEvent.AddListener(Example);
    }

    private void OnDisable()
    {
        exampleEvent.RemoveListener(Example);
    }

    void Start()
    {
        exampleEvent?.Invoke();
    }

    private void Example()
    {
        Debug.Log("Example");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheckInputEvent : UnityEvent<PlayerInput>
{
    
}

public class Events : MonoBehaviour
{
    public static UnityEvent exampleEvent = new UnityEvent();
    public static UnityEvent startBoat = new UnityEvent();
    public static UnityEvent stopBoat = new UnityEvent();
    public static CheckInputEvent checkInputEvent = new CheckInputEvent();

    private void OnEnable()
    {
        exampleEvent.AddListener(Example);
        checkInputEvent.AddListener(Test);
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

    void Test(PlayerInput input)
    {
        Debug.Log("test");
    }
}

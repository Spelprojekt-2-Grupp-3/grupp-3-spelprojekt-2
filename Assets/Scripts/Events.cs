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
    public static UnityEvent startPlayer = new UnityEvent();
    public static UnityEvent stopPlayer = new UnityEvent();
    //public static UnityEvent updateIcons = new UnityEvent();
    public static CheckInputEvent checkInputEvent = new CheckInputEvent();
    public static UnityEvent updateBoostMeter = new UnityEvent();
    public static UnityEvent BengtMinigameHit = new UnityEvent();
    public static UnityEvent<CurrentInputDevice, PlayerInput> setIcons = new UnityEvent<CurrentInputDevice, PlayerInput>();

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

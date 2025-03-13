using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable Objects/Current Input")]

public class CurrentInputIcons : ScriptableObject
{
    [HideInInspector] public CurrentInputDevice currentInputDevice = new CurrentInputDevice();
    public CurrentInputDevice playstation = new CurrentInputDevice();
    public CurrentInputDevice xbox = new CurrentInputDevice();
    public CurrentInputDevice keyboard = new CurrentInputDevice();
    
    private void OnEnable()
    {
        Events.checkInputEvent.AddListener(ChangeInputDevice);
        currentInputDevice = playstation;
    }

    private void OnDisable()
    {
        Events.checkInputEvent.RemoveListener(ChangeInputDevice);
    }

    private void ChangeInputDevice(PlayerInput sentInputDevice)
    {
        string deviceName = "";
        if (sentInputDevice.currentControlScheme.ToLower().Contains("keyboard"))
        {
            deviceName = "Keyboard";
            currentInputDevice = keyboard;
        }
        
        else if (sentInputDevice.currentControlScheme == "Gamepad")
        {
            deviceName = Gamepad.current.description.product;
            if (deviceName.ToLower().Contains("dualsense") || Gamepad.current.description.manufacturer.ToLower().Contains("sony"))
            {
                currentInputDevice = playstation;
            }
            else
            {
                currentInputDevice = xbox;
                Debug.Log("Prob xbox");
            }
        }
        Events.setIcons.Invoke(currentInputDevice, sentInputDevice);
    }
}

[Serializable]
public class CurrentInputDevice
{
    public Sprite moveSprite;
    public Sprite moveCameraSprite;
    public Sprite interactSprite;
    public Sprite gasSprite;
    public Sprite boostSprite;
    public Sprite reverseSprite;
    public Sprite buttonSouth;
    public Sprite buttonNorth;
    public Sprite buttonWest;
    public Sprite buttonEast;
    public Sprite startSprite;
    public Sprite selectSprite;
    public Sprite leftTriggerSprite;
    public Sprite rightTriggerSprite;
    public Sprite leftShoulderSprite;
    public Sprite rightShoulderSprite;
    public Sprite dpadSprite;
    public Sprite leftStickSprite;
    public Sprite rightStickSprite;    
}

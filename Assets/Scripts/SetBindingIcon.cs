using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetBindingIcon : MonoBehaviour
{
    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField]
    private InputActionReference m_Action;
    
    private void OnEnable()
    {
        Events.setIcons.AddListener(SetIcon);
    }

    private void SetIcon(CurrentInputDevice inputDevice, PlayerInput input)
    {
        var deviceLayoutName = default(string);
        var controlPath = default(string);
        var action = m_Action.action;
        int bindingIndex = action.GetBindingIndex(input.currentControlScheme.ToLower());
        if (bindingIndex == -1) return;
        action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
        var image = GetComponent<Image>();
         
        switch (controlPath)
        {
            case "buttonSouth":
                image.sprite = inputDevice.buttonSouth;
                return;
            case "buttonNorth": 
                image.sprite = inputDevice.buttonNorth;
                return;
            case "buttonEast": 
                image.sprite = inputDevice.buttonEast;
                return;
            case "buttonWest": 
                image.sprite = inputDevice.buttonWest;
                return;
            case "start":
                image.sprite = inputDevice.startSprite;
                return;
            case "select": 
                image.sprite = inputDevice.selectSprite;
                return;
            case "leftTrigger": 
                image.sprite = inputDevice.leftTriggerSprite;
                return;
            case "rightTrigger": 
                image.sprite = inputDevice.rightTriggerSprite;
                return;
            case "leftShoulder": 
                image.sprite = inputDevice.leftShoulderSprite;
                return;
            case "rightShoulder": 
                image.sprite = inputDevice.rightShoulderSprite;
                return;
            case "dpad": 
                image.sprite = inputDevice.dpadSprite;
                return;
            case "leftStick": 
                image.sprite = inputDevice.moveSprite;
                return;
            case "rightStick": 
                image.sprite = inputDevice.moveCameraSprite;
                return;
        }

        if (deviceLayoutName.ToLower() == "keyboard" || deviceLayoutName.ToLower() == "mouse")
        {
            if (action.actionMap.name.ToLower() == "boat")
            {
                switch (controlPath)
                {
                    case "s":
                        image.sprite = inputDevice.reverseSprite;
                        return;
                    case "w": 
                        image.sprite = inputDevice.gasSprite;
                        return;
                    case "d": 
                    case "a": 
                        image.sprite = inputDevice.moveSprite;
                        return;
                    case "delta": 
                        image.sprite = inputDevice.moveCameraSprite;
                        return;
                    case "escape":
                        image.sprite = inputDevice.startSprite;
                        return;
                    case "select": 
                        image.sprite = inputDevice.selectSprite;
                        return;
                    case "shift": 
                        image.sprite = inputDevice.rightShoulderSprite;
                        return;
                }
            }
            else
            {
                switch (controlPath)
                {
                    case "s":
                        image.sprite = inputDevice.buttonSouth;
                        return;
                    case "w": 
                        image.sprite = inputDevice.buttonNorth;
                        return;
                    case "d": 
                        image.sprite = inputDevice.buttonEast;
                        return;
                    case "a": 
                        image.sprite = inputDevice.buttonWest;
                        return;
                    case "escape":
                        image.sprite = inputDevice.startSprite;
                        return;
                    case "select": 
                        image.sprite = inputDevice.selectSprite;
                        return;
                    case "shift": 
                        image.sprite = inputDevice.rightShoulderSprite;
                        return;
                }
            }
        }
    }
}

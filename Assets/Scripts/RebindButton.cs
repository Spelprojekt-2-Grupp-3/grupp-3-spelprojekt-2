using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindButton : MonoBehaviour
{
    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField] private InputActionReference m_Action;
    [SerializeField] private GameObject button;
    [SerializeField] private Image icon;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private const string rebinds = "Rebinds";
    private void Awake()
    {
        // Loads rebinds
        string rebindString = PlayerPrefs.GetString(rebinds, string.Empty);
        if (string.IsNullOrEmpty(rebindString)) return;
        InputListener.Instance.playerInput.actions.LoadBindingOverridesFromJson(rebindString);
    }

    private void Start()
    {
        SetIcon();
    }

    public void Pressed()
    {
        button.SetActive(false);
        m_Action.action.Disable();
        rebindingOperation = m_Action.action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete(rebindingOperation)).Start();
    }
    

    private void RebindComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        button.SetActive(true);
        string actionName = m_Action.name;
        string rebindString = "";
        if (rebindingOperation.action.ToString().Contains("leftStick"))
        {
            rebindString = "{\"bindings\":[{\"action\":\""+actionName+"\",\"id\":\"759da2d7-7b15-4668-8b9c-e8d55b86c0b3\",\"path\":\"<Gamepad>/leftStick\",\"interactions\":\"null\",\"processors\":\"null\"},{\"action\":\"Boat/MinigameButtonSouth\",\"id\":\"86cf62c7-bbad-4c9a-b99b-e5318e762a62\",\"path\":\"<Gamepad>/leftStick/left\",\"interactions\":\"null\",\"processors\":\"null\"}]}";
        }
        
        else if (rebindingOperation.action.ToString().Contains("rightStick"))
        {
            rebindString = "{\"bindings\":[{\"action\":\""+actionName+"\",\"id\":\"759da2d7-7b15-4668-8b9c-e8d55b86c0b3\",\"path\":\"<Gamepad>/rightStick\",\"interactions\":\"null\",\"processors\":\"null\"},{\"action\":\"Boat/MinigameButtonSouth\",\"id\":\"86cf62c7-bbad-4c9a-b99b-e5318e762a62\",\"path\":\"<Gamepad>/leftStick/left\",\"interactions\":\"null\",\"processors\":\"null\"}]}";
        }

        else
        {
            rebindString = InputListener.Instance.playerInput.actions.SaveBindingOverridesAsJson();
        }
        PlayerPrefs.SetString(rebinds, rebindString);
        m_Action.action.Enable();
        rebindingOperation.Dispose();
        SetIcon();
    }

    private void SetIcon()
    {
        var deviceLayoutName = default(string);
        var controlPath = default(string);
        var action = m_Action.action;
        int bindingIndex = action.GetBindingIndex(InputListener.Instance.playerInput.currentControlScheme.ToLower());
        var inputDevice = InputListener.Instance.inputDevice.currentInputDevice;
        if (bindingIndex == -1) return;
        action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
        var image = icon;

        switch (controlPath)
        {
            case "buttonSouth":
                if (action.actionMap.name.ToLower() == "player")
                {
                    image.sprite = inputDevice.interactSprite;
                }
                else
                {
                    image.sprite = inputDevice.buttonSouth;
                }
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
            case "leftTriggerButton":
                image.sprite = inputDevice.reverseSprite;
                return;
            case "rightTriggerButton":
                image.sprite = inputDevice.gasSprite;
                return;
            case "rightShoulder":
                image.sprite = inputDevice.boostSprite;
                return;
            case string path when path.Contains("leftStick"):
                image.sprite = inputDevice.moveSprite;
                return;
            case string path when path.Contains("rightStick"):
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
                    case "e":
                        image.sprite = inputDevice.interactSprite;
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
                        image.sprite = inputDevice.boostSprite;
                        return;
                }
            }
            else
            {
                switch (controlPath)
                {
                    case "e":
                        image.sprite = inputDevice.interactSprite;
                        return;
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
                        image.sprite = inputDevice.boostSprite;
                        return;
                }
            }
        }
    }
}

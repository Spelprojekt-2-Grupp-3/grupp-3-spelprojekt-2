using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleUI : MonoBehaviour
{
    private bool hideUI;
    [SerializeField] private GameObject objectToHide;
    private PlayerInputActions playerControls;
    private InputAction toggleUI;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        toggleUI = playerControls.Boat.ToggleUI;
    }

    private void OnEnable()
    {
        toggleUI.Enable();
        toggleUI.performed += Toggle;
    }

    private void OnDisable()
    {
        toggleUI.Disable();
        toggleUI.performed -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (!hideUI)
        {
            hideUI = !hideUI;
            objectToHide.SetActive(false);
        }
        else
        {
            hideUI = !hideUI;
            objectToHide.SetActive(true);
        }
    }
}

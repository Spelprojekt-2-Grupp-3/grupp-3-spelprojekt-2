using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction pause;
    
    private void OnEnable()
    {
        pause = playerControls.UI.Pause;
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.UI.Pause.performed += ActivatePauseMenu;
    }

    private void ActivatePauseMenu(InputAction.CallbackContext context)
    {
        if(pause.WasPressedThisFrame()) {
            if(Time.timeScale == 0) {
                Time.timeScale = 1;
            }
            else {
                Time.timeScale = 0;
            }

            gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
        }
    }
}

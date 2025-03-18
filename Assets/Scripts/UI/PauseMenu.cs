using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction pause;
    private bool paused;
    [SerializeField] private Camera camera;
    [SerializeField] private RenderTexture renderTexture;
    private Camera cameraBrain;
    private GameObject playerMovement;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.UI.Pause.performed += ActivatePauseMenu;
        pause = playerControls.UI.Pause;
        pause.Enable();
        cameraBrain = Camera.main;
        playerMovement = GameObject.FindWithTag("Player");
    }

    private void ActivatePauseMenu(InputAction.CallbackContext context)
    {
        paused = !paused;
        if (paused)
        {
            //Time.timeScale = 0;
            
            if (playerMovement.activeSelf)
            {
                Events.stopPlayer?.Invoke();
            }
            else
            {
                Events.stopBoat.Invoke();
            }
            gameObject.GetComponent<Canvas>().enabled = true;
            camera.enabled = true;
            cameraBrain.targetTexture = renderTexture;
        }
        else
        {
            //Time.timeScale = 1;
            if (playerMovement.activeSelf)
            {
                Events.startPlayer?.Invoke();
            }
            else
            {
                Events.startBoat.Invoke();
            }
            gameObject.GetComponent<Canvas>().enabled = false;
            camera.enabled = false;
            cameraBrain.targetTexture = null;
        }
    }
}

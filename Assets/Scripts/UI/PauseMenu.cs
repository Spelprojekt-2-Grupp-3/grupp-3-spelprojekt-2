using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private GameObject optionsObject;
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
        if (!paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Pause()
    {
        paused = true;
        if (playerMovement.activeSelf)
        {
            Events.stopPlayer?.Invoke();
        }
        else
        {
            Events.stopBoat.Invoke();
        }
        gameObject.GetComponent<Canvas>().enabled = true;
        EventSystem.current.SetSelectedGameObject(firstSelected);
        camera.enabled = true;
        cameraBrain.targetTexture = renderTexture;
    }

    public void Resume()
    {
        paused = false;
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
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void Options()
    {
        optionsObject.SetActive(!optionsObject.activeSelf);
    }
}

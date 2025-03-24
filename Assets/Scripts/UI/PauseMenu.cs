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
    private InputAction pause, cancel;
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
        cancel = playerControls.UI.Cancel;
        cancel.Disable();
    }

    private void ActivatePauseMenu(InputAction.CallbackContext context)
    {
        var minigames = FindObjectsOfType<Minigames>();
        foreach (var minigame in minigames)
        {
            minigame.CloseMinigame(context);
        }
        if (!paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    private void Pause()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().PauseDialogue();
        }
        DialogueManager.GetInstance().submit.Disable();
        cancel.Enable();
        playerControls.UI.Cancel.performed += TryCancel;
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
        DialogueManager.GetInstance().submit.Enable();
        cancel.Disable();
        playerControls.UI.Cancel.performed -= TryCancel;
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
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().ResumeDialogue();
        }
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void Options()
    {
        optionsObject.SetActive(!optionsObject.activeSelf);
        if (optionsObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(optionsObject.transform.GetChild(0).gameObject);
        }
    }

    private void TryCancel(InputAction.CallbackContext context)
    {
        if (optionsObject.activeSelf)
        {
            optionsObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
        else if (optionsObject.transform.GetChild(0).gameObject.activeSelf)
        {
            optionsObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (gameObject.GetComponent<Canvas>().enabled)
        {
            Resume();
        }
    }
}

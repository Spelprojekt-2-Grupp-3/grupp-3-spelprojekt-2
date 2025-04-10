using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelected;

    [SerializeField]
    private GameObject optionsObject;
    private PlayerInputActions playerControls;
    private InputAction pause,
        cancel;
    private bool paused;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private RenderTexture renderTexture;
    private Camera cameraBrain;
    private GameObject playerMovement;
    private IslandBoarding[] islandBoardings;

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
        islandBoardings = FindObjectsOfType<IslandBoarding>();
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
            Cursor.lockState = CursorLockMode.None;
            Pause();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Resume();
        }
    }

    private void Pause()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().PauseDialogue();
        }
        DialogueManager.GetInstance().submit.Disable();
        foreach (var boarding in islandBoardings)
        {
            boarding.board.Disable();
        }
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
        Cursor.lockState = CursorLockMode.Locked;
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
        foreach (var boarding in islandBoardings)
        {
            boarding.board.Enable();
        }
    }

    public void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void Options()
    {
        optionsObject.SetActive(!optionsObject.activeSelf);
        if (optionsObject.activeSelf)
        {
            optionsObject.transform.GetChild(0).gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(
                optionsObject.transform.GetChild(0).gameObject
            );
        }
    }

    private void TryCancel(InputAction.CallbackContext context)
    {
        if (optionsObject.activeSelf)
        {
        //    Debug.Log("optionsmenu active");
            optionsObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
        else if (gameObject.GetComponent<Canvas>().enabled)
        {
         //   Debug.Log("tried to unpause");
            Resume();
        }
    }
}

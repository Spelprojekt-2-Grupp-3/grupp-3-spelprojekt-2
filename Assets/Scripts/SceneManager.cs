using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using FMODUnity;
using FMOD.Studio;

public class SceneManager : MonoBehaviour
{
    private string s = "";
    private PlayerInputActions playerInputActions;
    private InputAction navigate;
    private InputDevice inputDevice;
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject exit;
    private InputDevice previousDevice = null;
    private PlayerInput playerInput;

    public StudioEventEmitter menuTheme;
    public StudioEventEmitter creditsTheme;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnable()
    {
        navigate = playerInputActions.UI.Navigate;
        navigate.Enable();
        playerInput.onControlsChanged += CheckInputDevice;
    }

    private void OnDisable()
    {
        navigate.Disable();
        playerInput.onControlsChanged -= CheckInputDevice;
    }
    
    public void Load(string s)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(s);
    }

    public void Exit()
    {
        Debug.Log("fucker");
        Application.Quit();
    }

    public void ToggleElement(GameObject g)
    {
        g.SetActive(!g.active);

        if (g.active)
            s = "Close";
        else
            s = "Credits";
    }

    public void ToggleMusic()
    {
        if (creditsTheme.IsPlaying())
        {
            creditsTheme.Stop();
            menuTheme.Play();
        }
        else
        {
            menuTheme.Stop();
            creditsTheme.Play();
        }
    }

    public void ChangeTextElement(GameObject tmp)
    {
        tmp.GetComponent<TextMeshProUGUI>().text = s;
    }

    private void CheckInputDevice(PlayerInput input)
    {
        if (input.currentControlScheme.ToLower().Contains("mouse"))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
    }
}

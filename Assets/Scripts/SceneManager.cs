using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManager : MonoBehaviour
{
    private string s = "";
    private PlayerInputActions playerInput;
    private InputAction navigate;
    private InputDevice inputDevice;
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject exit;
    private InputDevice previousDevice = null;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnable()
    {
        navigate = playerInput.UI.Navigate;
        navigate.Enable();
        navigate.performed += CheckInputDevice;
    }

    private void OnDisable()
    {
        navigate.Disable();
        navigate.performed -= CheckInputDevice;
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

    public void ChangeTextElement(GameObject tmp)
    {
        tmp.GetComponent<TextMeshProUGUI>().text = s;
    }

    private void CheckInputDevice(InputAction.CallbackContext context)
    {
        if (Mouse.current != null && 
            (Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame ||
             Mouse.current.middleButton.wasPressedThisFrame ||
             Mouse.current.delta.ReadValue() != Vector2.zero)) // Detect movement
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (EventSystem.current.currentSelectedGameObject == null && Gamepad.current != null &&
                 Gamepad.current.leftStick.ReadValue() != Vector2.zero)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
    }
}

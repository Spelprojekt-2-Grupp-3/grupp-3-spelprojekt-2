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
    [SerializeField] private GameObject firstSelected;
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
        if (context.control.device is Mouse)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if(context.control.device is not Mouse && previousDevice is Mouse) // Nuvarande input device är inte en mus och om den tidigare var mus så markerar den första objektet.
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }

        previousDevice = context.control.device;
    }
}

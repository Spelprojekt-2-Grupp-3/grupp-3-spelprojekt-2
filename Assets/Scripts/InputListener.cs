using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public static InputListener Instance { get; private set; }

    [HideInInspector]
    public PlayerInput playerInput;
    public CurrentInputIcons inputDevice;

    private void OnEnable()
    {
        playerInput.onControlsChanged += ChangeDevice;
    }

    private void OnDisable()
    {
        playerInput.onControlsChanged -= ChangeDevice;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Events.checkInputEvent?.Invoke(playerInput);
    }

    private void ChangeDevice(PlayerInput input)
    {
        Events.checkInputEvent?.Invoke(input);
        Debug.Log("changed device");
    }
}

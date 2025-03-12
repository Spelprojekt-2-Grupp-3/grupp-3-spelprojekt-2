using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SlideshowManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] panels;
    public EventReference selectSound;
    private SceneManager sceneManager;
    private int currentPanelIndex = 0;
    private PlayerInputActions _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        image = GetComponent<Image>();
        sceneManager = FindObjectOfType<SceneManager>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        image.sprite = panels[currentPanelIndex];
    }

    private void Update()
    {
        if  (_playerInput.UI.NavigateRight.WasPressedThisFrame())
        {
            NextPanel();
        }
        else if (_playerInput.UI.NavigateLeft.WasPressedThisFrame())
        {
            PreviousPanel();
        }
    }

    private void NextPanel()
    {
        if (currentPanelIndex < panels.Length - 1) // Prevents going out of bounds
        {
            currentPanelIndex++;
            image.sprite = panels[currentPanelIndex];
            OnSelect();
        }
        else
        {
            sceneManager.Load("Main Scene");
            Debug.Log("Slideshow finished!");
            OnSelect();
        }
    }
    
    private void PreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            image.sprite = panels[currentPanelIndex];
            OnSelect();
        }
    }
    
    public void OnSelect() 
    {
        RuntimeManager.PlayOneShot(selectSound);
    }
}
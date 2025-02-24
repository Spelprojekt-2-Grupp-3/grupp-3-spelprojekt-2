using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TimingMinigame : Minigames
{
    [SerializeField] private GameObject canvas;
    private GameObject canvasInst;
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigameButtonSouth;
    [SerializeField] private CurrentInputIcons inputIcons;
    [SerializeField] private GameObject sliderObj;
    private GameObject sliderInst;
    private Slider slider;
    private Sprite iconSprite;
    [SerializeField] private float maxTime;
    private float timer;

    [SerializeField, Range(0f, 10f), Tooltip("Amount of seconds it takes for the handle on the slider to go from the bottom to the top")]
    private float sliderMaxValue;

    private bool increasing;

    private void OnEnable()
    {
        camera = Camera.main;
        Events.updateIcons.AddListener(UpdateIcons);
    }

    private void OnDisable()
    {
        Events.updateIcons.RemoveListener(UpdateIcons);
    }

    private void Awake()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        increasing = true;
        canvasInst = Instantiate(canvas);
        var canvasComponent = canvasInst.GetComponent<Canvas>();
        canvasComponent.worldCamera = camera;
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        sliderInst = Instantiate(sliderObj, canvasInst.transform);
        slider = sliderInst.GetComponent<Slider>();
        slider.maxValue = sliderMaxValue;
        iconSprite = GameObject.Find("Handle").GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (increasing)
        {
            slider.value += Time.deltaTime;
            if (slider.value >= sliderMaxValue)
            {
                increasing = false;
            }
        }
        else
        {
            slider.value -= Time.deltaTime;
            if (slider.value <= 0)
            {
                increasing = true;
            }
        }
        timer += Time.deltaTime;
    }

    private void UpdateIcons()
    {
        iconSprite = inputIcons.currentInputDevice.buttonSouth;
    }
}

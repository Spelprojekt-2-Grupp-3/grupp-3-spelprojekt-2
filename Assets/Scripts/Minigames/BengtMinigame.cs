using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimingMinigame : Minigames
{
    private GameObject canvasInst, handle, goal;
    [SerializeField] private GameObject sliderInst;
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigameButtonSouth;
    [SerializeField] private CurrentInputIcons inputIcons;
    [SerializeField] private GameObject sliderObj;
    private Slider slider;
    private Image iconSprite;
    [SerializeField, Tooltip("Max time for the minigame to play, after this time you lose")] private float maxTime;
    private Vector2 goalPos;
    private float goalHeight, sliderSpeed, timer;
    [SerializeField, Range(0f, 10f), Tooltip("Initial amount of seconds it takes for the handle on the slider to go from the bottom to the top")]
    private float sliderMaxValue;
    private bool increasing;

    private void OnEnable()
    {
        camera = Camera.main;
        Events.updateIcons.AddListener(UpdateIcons);
        minigameButtonSouth = playerControls.Boat.MinigameButtonSouth;
        minigameButtonSouth.Enable();
    }

    private void OnDisable()
    {
        Events.updateIcons.RemoveListener(UpdateIcons);
        minigameButtonSouth.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        canvasInst = gameObject;
        StartMinigame();
    }

    private void Start()
    {
        
    }

    public override void StartMinigame()
    {
        sliderSpeed = 1.5f;
        increasing = true;
        var canvasComponent = GetComponent<Canvas>();
        canvasComponent.worldCamera = camera;
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        slider = sliderInst.GetComponent<Slider>();
        slider.maxValue = sliderMaxValue;
        handle = sliderInst.transform.Find("Handle Slide Area").transform.Find("Handle").gameObject;
        iconSprite = handle.GetComponent<Image>();
        goal = sliderInst.transform.Find("Goal").gameObject;
        RandomizeGoalPosition();
    }

    private void RandomizeGoalPosition()
    {
        goalHeight = goal.GetComponent<RectTransform>().rect.height;
        var rectHeight = sliderInst.GetComponent<RectTransform>().rect.height;
        goalPos = new Vector2(goal.transform.localPosition.x, Random.Range(-rectHeight / 2 + goalHeight / 2, rectHeight / 2 - goalHeight / 2));
        goal.transform.localPosition = goalPos;
    }

    private void Update()
    {
        UpdateSliderPos();
        if (minigameButtonSouth.WasPressedThisFrame())
        {
            if (handle.transform.position.y <= goal.transform.position.y + goalHeight/2 && handle.transform.position.y >= goal.transform.position.y - goalHeight/2) // idk why divided by 3 is necessary tbh
            {
                Debug.Log("hit");
                sliderSpeed *= 1.2f;
                RandomizeGoalPosition();
            }
        }
        timer += Time.deltaTime;
    }

    private void UpdateSliderPos()
    {
        if (increasing)
        {
            slider.value += Time.deltaTime * sliderSpeed;
            if (slider.value >= sliderMaxValue)
            {
                increasing = false;
            }
        }
        else
        {
            slider.value -= Time.deltaTime * sliderSpeed;
            if (slider.value <= 0)
            {
                increasing = true;
            }
        }
    }

    private void UpdateIcons()
    {
        iconSprite.sprite = inputIcons.currentInputDevice.buttonSouth;
    }
}

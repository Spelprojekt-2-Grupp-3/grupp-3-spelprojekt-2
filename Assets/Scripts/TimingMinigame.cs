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
    [SerializeField] private GameObject canvas;
    private GameObject canvasInst;
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigameButtonSouth;
    [SerializeField] private CurrentInputIcons inputIcons;
    [SerializeField] private GameObject sliderObj;
    private GameObject sliderInst;
    private Slider slider;
    private Image iconSprite;
    [SerializeField] private float maxTime;
    private float timer;
    private Vector2 goalPos;
    private GameObject handle;
    private GameObject goal;
    private float goalHeight;
    private float sliderSpeed;
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
        StartMinigame();
    }

    public override void StartMinigame()
    {
        sliderSpeed = 1.5f;
        increasing = true;
        canvasInst = Instantiate(canvas);
        var canvasComponent = canvasInst.GetComponent<Canvas>();
        canvasComponent.worldCamera = camera;
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        sliderInst = Instantiate(sliderObj, canvasInst.transform);
        slider = sliderInst.GetComponent<Slider>();
        slider.maxValue = sliderMaxValue;
        handle = sliderInst.transform.Find("Handle Slide Area").transform.Find("Handle").gameObject;
        iconSprite = handle.GetComponent<Image>();
        goal = sliderInst.transform.Find("Goal").gameObject;
        RandomizeGoalPosition();
    }

    private void RandomizeGoalPosition()
    {
        var rectHeight = sliderInst.GetComponent<RectTransform>().rect.height;
        goalPos = new Vector2(goal.transform.localPosition.x, Random.Range(-rectHeight / 2, rectHeight / 2));
        goal.transform.localPosition = goalPos;
        goalHeight = goal.GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        UpdateSliderPos();
        if (minigameButtonSouth.WasPressedThisFrame())
        {
            if (handle.transform.position.y <= goal.transform.position.y + goalHeight/3 && handle.transform.position.y >= goal.transform.position.y - goalHeight/3) // idk why divided by 3 is necessary tbh
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

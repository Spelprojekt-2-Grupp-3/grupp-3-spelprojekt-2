using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BengtMinigame : Minigames
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
    public EventReference engineStage1, engineStage2, engineStage3;
    private int engineStage;

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
        Events.stopPlayer?.Invoke();
        engineStage = 0;
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
        UpdateIcons();
        Events.stopBoat?.Invoke();
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
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
            if (handle.transform.position.y <= goal.transform.position.y + goalHeight && handle.transform.position.y >= goal.transform.position.y - goalHeight) // idk why divided by 3 is necessary tbh
            {
                Events.BengtMinigameHit?.Invoke();
                engineStage++;
                switch (engineStage)
                {
                    case 1:
                        RuntimeManager.PlayOneShot(engineStage1);
                        break;
                    case 2:
                        RuntimeManager.PlayOneShot(engineStage2);
                        break;
                    case 3:
                        RuntimeManager.PlayOneShot(engineStage3);
                        StopMinigame();
                        break;
                }
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

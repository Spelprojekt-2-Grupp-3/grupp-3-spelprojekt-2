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
    [SerializeField] private GameObject handle, goal;
    [SerializeField] private GameObject sliderInst;
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigameButtonSouth, minigameButtonEast;
    private Slider slider;
    private Image iconSprite;
    [SerializeField, Tooltip("Max time for the minigame to play, after this time you lose")] private float maxTime;
    private Vector2 goalPos;
    private float goalHeight, sliderSpeed;
    [SerializeField, Range(0f, 10f), Tooltip("Initial amount of seconds it takes for the handle on the slider to go from the bottom to the top")]
    private float sliderMaxValue;
    private bool increasing;
    public EventReference engineStage1, engineStage2, engineStage3;
    private int engineStage;

    private void OnEnable()
    {
        minigameButtonSouth = playerControls.UI.Submit;
        minigameButtonSouth.Enable();
        minigameButtonEast = playerControls.UI.ButtonEast;
        minigameButtonEast.Enable();
        minigameButtonEast.performed += CloseMinigame;
        Events.stopPlayer?.Invoke();
    }

    private void OnDisable()
    {
        minigameButtonSouth.Disable();
        minigameButtonEast.Disable();
        minigameButtonEast.performed -= CloseMinigame;
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        StartMinigame();
    }

    public override void StartMinigame()
    {
        engineStage = 0;
        sliderSpeed = 1.5f;
        increasing = true;
        slider = sliderInst.GetComponent<Slider>();
        slider.maxValue = sliderMaxValue;
        goal = sliderInst.transform.Find("Goal").gameObject;
        RandomizeGoalPosition();
        Events.stopBoat?.Invoke();
    }

    public override void StopMinigame()
    {
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
    }

    public override void CloseMinigame(InputAction.CallbackContext context)
    {
        Events.startPlayer?.Invoke();
        gameObject.SetActive(false);
    }

    private void RandomizeGoalPosition()
    {
        goalHeight = goal.GetComponent<RectTransform>().rect.height;
        var rectHeight = sliderInst.GetComponent<RectTransform>().rect.height;
        goalPos = new Vector2(goal.transform.localPosition.x, Random.Range(-rectHeight / 2 + goalHeight, rectHeight / 2 - goalHeight));
        goal.transform.localPosition = goalPos;
    }

    private void Update()
    {
        UpdateSliderPos();
        if (minigameButtonSouth.WasPressedThisFrame())
        {
            if (handle.transform.position.y <= goal.transform.position.y + goalHeight && handle.transform.position.y >= goal.transform.position.y - goalHeight)
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
}

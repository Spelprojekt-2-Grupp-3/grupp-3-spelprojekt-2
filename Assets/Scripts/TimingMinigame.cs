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
    private Sprite iconSprite;
    [SerializeField] private float maxTime;
    private float timer;
    private Vector2 goalPos;
    private GameObject handle;
    private GameObject goal;

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
        handle = sliderInst.transform.Find("Handle Slide Area").transform.Find("Handle").gameObject;
        iconSprite = handle.GetComponent<Image>().sprite;
        goal = sliderInst.transform.Find("Goal").gameObject;
        var rectHeight = sliderInst.GetComponent<RectTransform>().rect.height;
        goalPos = new Vector2(goal.transform.localPosition.x, Random.Range(-rectHeight / 2, rectHeight / 2));
        goal.transform.localPosition = goalPos;
    }

    private void Update()
    {
        UpdateSliderPos();
        //Debug.Log(sliderInst.transform.Find("Handle Slide Area").transform.Find("Handle").transform.position.y);
        //Debug.Log(sliderInst.transform.Find("Goal").gameObject.transform.position.y);
        if (handle.transform.position.y <=
            goal.transform.position.y + 5 &&
            handle.transform.position.y >=
            goal.transform.position.y - 5)
        {
            Debug.Log("hit");
        }
        timer += Time.deltaTime;
    }

    private void UpdateSliderPos()
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
    }

    private void UpdateIcons()
    {
        iconSprite = inputIcons.currentInputDevice.buttonSouth;
    }
}

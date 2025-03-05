using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class IrmaMinigame : Minigames
{
    private PlayerInputActions playerControls;
    private InputAction leftStick, rightStick, submit;
    [SerializeField] private Canvas irmaMinigameCanvas;
    private Image leftStickIcon, rightStickIcon, submitIcon;
    private int targetValueLeft, targetValueRight, currentValueLeft, currentValueRight;
    [SerializeField] private CurrentInputIcons currentInput;
    [SerializeField, Range(0, 100f)] private int numberRange;
    private TextMeshProUGUI currentValueLeftInst, currentValueRightInst, targetLeftInst, targetRightInst;
    private bool hasStarted = false;

    private void OnEnable()
    {
        leftStick = playerControls.Player.Move;
        leftStick.Enable();
        rightStick = playerControls.Player.Look;
        rightStick.Enable();
        submit = playerControls.UI.Submit;
        submit.Enable();
    }

    private void OnDisable()
    {
        leftStick.Disable();
        rightStick.Disable();
        submit.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        var canvasInst = Instantiate(irmaMinigameCanvas);
        var bg = canvasInst.transform.Find("Background");
        targetLeftInst = bg.Find("TargetValueLeft").GetComponent<TextMeshProUGUI>();
        targetRightInst = bg.Find("TargetValueRight").GetComponent<TextMeshProUGUI>();
        currentValueLeftInst = bg.Find("LeftValue").GetComponent<TextMeshProUGUI>();
        currentValueRightInst = bg.Find("RightValue").GetComponent<TextMeshProUGUI>();
        leftStickIcon = bg.Find("LeftStickIcon").GetComponent<Image>();
        rightStickIcon = bg.Find("RightStickIcon").GetComponent<Image>();
        submitIcon = bg.Find("SubmitIcon").GetComponent<Image>();
        targetValueLeft = Random.Range(0, numberRange);
        targetValueRight = Random.Range(0, numberRange);
        currentValueLeft = 0;
        currentValueRight = 0;
        UpdateText();
        hasStarted = true;
    }

    public override void StopMinigame()
    {
        Debug.Log("winner winner chicken dinner");
    }

    private void Update()
    {
        if (submit.WasPerformedThisFrame())
        {
            if (currentValueLeft == targetValueLeft && currentValueRight == targetValueRight)
            {
                StopMinigame();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasStarted) return;
        if (leftStick.IsInProgress())
        {
            currentValueLeft += Mathf.RoundToInt(leftStick.ReadValue<Vector2>().x);
            if (currentValueLeft > numberRange)
            {
                currentValueLeft = 0;
            }
            else if (currentValueLeft < 0)
            {
                currentValueLeft = numberRange;
            }
            UpdateText();
        }

        if (rightStick.IsInProgress())
        {
            currentValueRight += Mathf.RoundToInt(rightStick.ReadValue<Vector2>().x);
            if (currentValueRight > numberRange)
            {
                currentValueRight = 0;
            }
            else if (currentValueRight < 0)
            {
                currentValueRight = numberRange;
            }
            UpdateText();
        }
    }

    private void UpdateText()
    {
        targetLeftInst.text = targetValueLeft.ToString();
        targetRightInst.text = targetValueRight.ToString();
        currentValueLeftInst.text = currentValueLeft.ToString();
        currentValueRightInst.text = currentValueRight.ToString();
    }


    private void UpdateIcons()
    {
        leftStickIcon.sprite = currentInput.currentInputDevice.moveSprite;
        rightStickIcon.sprite = currentInput.currentInputDevice.moveCameraSprite;
        submitIcon.sprite = currentInput.currentInputDevice.interactSprite;
    }
}

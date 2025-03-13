using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
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
    [SerializeField] private Image leftStickIcon, rightStickIcon, submitIcon;
    private float currentValueLeft, currentValueRight;
    private int targetValueLeft, targetValueRight;
    [SerializeField] private CurrentInputIcons currentInput;
    [SerializeField, Range(0, 100)] private int numberRange;
    [SerializeField] private TextMeshProUGUI currentValueLeftInst, currentValueRightInst, targetLeftInst, targetRightInst;
    [SerializeField] private RectTransform  leftStickControl, rightStickControl;
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
        var brain = Camera.main.GetComponent<CinemachineBrain>(); 
        brain.enabled = false;
        Events.stopPlayer?.Invoke();
        var canvasInst = gameObject;
        targetValueLeft = Random.Range(1, numberRange);
        targetValueRight = Random.Range(1, numberRange);
        currentValueLeft = 0;
        currentValueRight = 0;
        UpdateText();
        hasStarted = true;
    }

    public override void StopMinigame()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>(); 
        brain.enabled = true;
        Events.startPlayer?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (submit.WasPerformedThisFrame())
        {
            if (Mathf.RoundToInt(currentValueLeft) == targetValueLeft && Mathf.RoundToInt(currentValueRight) == targetValueRight)
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
                currentValueLeft = numberRange;
            }
            else if (currentValueLeft < 0)
            {
                currentValueLeft = 0;
            }
            UpdateText();
        }
        
        var euler = leftStickControl.rotation.eulerAngles;
        
        euler.z = Mathf.Lerp(141f, -141f, currentValueLeft / numberRange);

        leftStickControl.rotation = Quaternion.Euler(euler);

        if (rightStick.IsInProgress())
        {
            currentValueRight += Mathf.RoundToInt(rightStick.ReadValue<Vector2>().x);
            if (currentValueRight > numberRange)
            {
                currentValueRight = numberRange;
            }
            else if (currentValueRight < 0)
            {
                currentValueRight = 0;
            }
            UpdateText();
        }
        
        euler = rightStickControl.rotation.eulerAngles;
        
        euler.z = Mathf.Lerp(141f, -141f, currentValueRight / numberRange);

        rightStickControl.rotation = Quaternion.Euler(euler);
    }

    private void UpdateText()
    {
        targetLeftInst.text = targetValueLeft.ToString();
        targetRightInst.text = targetValueRight.ToString();
        currentValueLeftInst.text = Mathf.RoundToInt(currentValueLeft).ToString();
        currentValueRightInst.text = Mathf.RoundToInt(currentValueRight).ToString();
        
    }


    private void UpdateIcons()
    {
        leftStickIcon.sprite = currentInput.currentInputDevice.moveSprite;
        rightStickIcon.sprite = currentInput.currentInputDevice.moveCameraSprite;
        submitIcon.sprite = currentInput.currentInputDevice.interactSprite;
    }
}

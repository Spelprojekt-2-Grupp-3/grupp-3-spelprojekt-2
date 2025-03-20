using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class IrmaMinigame : Minigames
{
    private PlayerInputActions playerControls;
    private InputAction leftStick, rightStick, submit, exit;
    [SerializeField] private Image leftStickIcon, rightStickIcon, submitIcon;
    private float currentValueLeft, currentValueRight;
    private int targetValueLeft, targetValueRight;
    [SerializeField, Range(0, 100)] private int numberRange;
    [SerializeField] private TextMeshProUGUI currentValueLeftInst, currentValueRightInst, targetLeftInst, targetRightInst;
    [SerializeField] private RectTransform  leftStickControl, rightStickControl;
    public EventReference radioSound;
    private FMOD.Studio.EventInstance radioSoundInstance;
    private bool hasStarted = false;

    private void OnEnable()
    {
        radioSoundInstance = RuntimeManager.CreateInstance(radioSound);
        radioSoundInstance.start();
        leftStick = playerControls.UI.LeftJoyStick;
        leftStick.Enable();
        rightStick = playerControls.UI.RightJoystick;
        rightStick.Enable();
        submit = playerControls.UI.ButtonWest;
        submit.Enable();
        exit = playerControls.UI.ButtonEast;
        exit.Enable();
        exit.performed += CloseMinigame;
    }

    private void OnDisable()
    {
        radioSoundInstance.stop(0);
        leftStick.Disable();
        rightStick.Disable();
        submit.Disable();
        exit.Disable();
        exit.performed -= CloseMinigame;
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        questLog = FindObjectOfType<QuestLog>();
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
        targetValueLeft = Random.Range(1, numberRange);
        targetValueRight = Random.Range(1, numberRange);
        currentValueLeft = 0;
        currentValueRight = 0;
        UpdateText();
        hasStarted = true;
    }
    
    public override void CloseMinigame(InputAction.CallbackContext context)
    {
        Events.startPlayer?.Invoke();
        gameObject.SetActive(false);
    }

    public override void StopMinigame()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>(); 
        brain.enabled = true;
        Events.startPlayer?.Invoke();
        questLog.UpdateQuest(ID,step);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!hasStarted) return;
        if (submit.WasPerformedThisFrame())
        {
            if (Mathf.RoundToInt(currentValueLeft) == targetValueLeft && Mathf.RoundToInt(currentValueRight) == targetValueRight)
            {
                StopMinigame();
            }
        }

        var value = 0.5f * Mathf.Min(1, currentValueLeft / targetValueLeft) + 
                    0.5f * Mathf.Min(1, currentValueRight / targetValueRight);
        radioSoundInstance.setParameterByName("Radio thing", value);
    }

    private void FixedUpdate()
    {
        if (!hasStarted) return;
        if (leftStick.IsInProgress())
        {
            Debug.Log("left stick");
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
            Debug.Log("right stick");
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
}

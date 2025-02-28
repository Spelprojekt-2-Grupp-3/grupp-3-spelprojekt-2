using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BuoyantObject))]
public class BoatMovement : MonoBehaviour
{
    public float moveSpeed {get; private set; }
    [SerializeField, Range(1, 200)] [Tooltip("Acceleration")]
    private float acceleration = 100;
    [SerializeField, Range(1, 100)] [Tooltip("Acceleration when reversing")]
    private float reverseAcceleration = 50;

    [SerializeField, Range(0f, 20f), Tooltip("Max duration for the boost in seconds")] private float maxBoostDuration;
    [SerializeField, Range(0f, 1f), Tooltip("How many seconds of boost will get refilled in a second")] private float boostRefillPerSecond;
    private float boostMeter;
    [SerializeField, Range(0f, 100f), Tooltip("Rotationspeed")] private float rotationSpeed = 1f;
    [SerializeField, Range(0f, 2500f)] private float maxSpeed = 2000f;
    [SerializeField, Range(-1250, 0f)] private float maxReverseSpeed = 1000f;
    [SerializeField, Range(0, 90)] private int sideTiltAngle = 25;
    [SerializeField, Range(0, 90)] private int frontTiltAngle = 25;
    [SerializeField, Range(0f, 10f)] private float tiltSpeed = 1f;
    public FMODUnity.EventReference boatSoundEvent;
    [SerializeField] private GameObject pauseMenu;

    private bool fillMeter;
    
    private FMOD.Studio.EventInstance boatSound;
    private PlayerInputActions playerControls;
    private InputAction move, gas, reverse, boost, look;
    private InputAction pause;
    private int moveDirection;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private BuoyantObject buoyancy;
    [SerializeField] private GameObject boostMeterObj;
    private Image boostMeterImage;
    
    private void Awake()
    {
        boostMeterImage = boostMeterObj.GetComponent<Image>();
        buoyancy = GetComponent<BuoyantObject>();
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        moveSpeed = 0;
        boostMeter = maxBoostDuration;
        fillMeter = false;
    }

    private void OnEnable()
    {
        move = playerControls.Boat.Move;
        move.Enable();
        gas = playerControls.Boat.Gas;
        gas.Enable();
        reverse = playerControls.Boat.Reverse;
        reverse.Enable();
        boost = playerControls.Boat.Boost;
        boost.Enable();
        look = playerControls.Boat.Look;
        look.Enable();
        pause = playerControls.UI.Pause;
        pause.Enable();
        Events.startBoat.AddListener(AllowMovement);
        Events.stopBoat.AddListener(DisallowMovement);
        playerInput.onControlsChanged += ChangeDevice;
    }

    private void OnDisable()
    {
        move.Disable();
        gas.Disable();
        reverse.Disable();
        boost.Disable();
        pause.Disable();
        Events.startBoat.RemoveListener(AllowMovement);
        Events.stopBoat.RemoveListener(DisallowMovement);
        playerInput.onControlsChanged -= ChangeDevice;
    }

    void Start()
    {
        Events.checkInputEvent?.Invoke(playerInput);
        boatSound = FMODUnity.RuntimeManager.CreateInstance(boatSoundEvent);
        boatSound.start();
    }
    
    void Update()
    {
        boatSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rb));

        if(pause.WasPressedThisFrame()) {
            if(Time.timeScale == 0) {
                Time.timeScale = 1;
            }
            else {
                Time.timeScale = 0;
            }
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        if (!boost.inProgress || boostMeter < 0f) fillMeter = true;
        
        if (boost.inProgress && boostMeter > Time.deltaTime)
        {
            fillMeter = false;
            boostMeter -= Time.deltaTime;
            Events.updateBoostMeter?.Invoke();
            boostMeterImage.fillAmount = boostMeter / maxBoostDuration;
            moveSpeed += acceleration * 4 * Time.deltaTime;
            if (moveSpeed > maxSpeed * 2)
            {
                moveSpeed = maxSpeed * 2;
            }

            if (boostMeter < 0f)
            {
                fillMeter = true;
            }
        }
        
        else if (gas.inProgress && !reverse.inProgress)
        {
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += acceleration * gas.ReadValue<float>() * Time.deltaTime;
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, 0.8f * Time.deltaTime);
            }
        }
        else if (reverse.inProgress && !gas.inProgress)
        {
            moveSpeed -= reverseAcceleration * reverse.ReadValue<float>() * Time.deltaTime;
            if (moveSpeed < maxReverseSpeed)
            {
                moveSpeed = maxReverseSpeed;
            }
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.8f * Time.deltaTime);
        }

        if (fillMeter)
        {
            boostMeter += Time.deltaTime * boostRefillPerSecond;
            boostMeterImage.fillAmount = boostMeter / maxBoostDuration;
            Events.updateBoostMeter?.Invoke();
            if (boostMeter > maxBoostDuration)
            {
                boostMeter = maxBoostDuration;
            }
        }
        float boatSpeed = moveSpeed * 15 / maxSpeed;
        boatSound.setParameterByName("Speed", boatSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 euler = transform.localEulerAngles;

        float tiltMoveSpeed = moveSpeed;
        if (tiltMoveSpeed > maxSpeed)
        {
            tiltMoveSpeed = maxSpeed;
        }
        float targetAngleX = tiltMoveSpeed * frontTiltAngle / maxSpeed;
        float targetRot = -targetAngleX;
        euler.x = Mathf.LerpAngle(euler.x, targetRot, tiltSpeed*Time.deltaTime);

        float targetRotationSpeed = moveSpeed * rotationSpeed / maxSpeed;
        euler.y += targetRotationSpeed * move.ReadValue<Vector2>().x;

        float targetAngleZ = -tiltMoveSpeed * sideTiltAngle * move.ReadValue<Vector2>().x / maxSpeed;
        euler.z = Mathf.LerpAngle(euler.z, targetAngleZ, tiltSpeed * Time.deltaTime);
        rb.rotation = Quaternion.Euler(euler);
        
        if (moveSpeed == 0 && !buoyancy.subm) return;
        rb.velocity = new Vector3(
             moveSpeed * transform.forward.x, rb.velocity.y,
             moveSpeed * transform.forward.z);
    }

    public void AllowMovement()
    {
        move.Enable();
        gas.Enable();
    }

    public void DisallowMovement()
    {
        move.Disable();
        gas.Disable();
    }

    private void ChangeDevice(PlayerInput input)
    {
        Events.checkInputEvent?.Invoke(input);
    }
}

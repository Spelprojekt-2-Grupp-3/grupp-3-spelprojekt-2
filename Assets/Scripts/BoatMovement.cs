using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    public FMODUnity.EventReference boatSoundEvent, boatWaterSoundEvent;

    private bool fillMeter;
    
    private FMOD.Studio.EventInstance boatSound, boatWaterSound;
    private PlayerInputActions playerControls;
    private InputAction move, gas, reverse, boost;
    private int moveDirection;
    private Rigidbody rb;
    private BuoyantObject buoyancy;
    [SerializeField] private GameObject boostMeterObj;
    private Image boostMeterImage;
    
    private void Awake()
    {
        boostMeterImage = boostMeterObj.GetComponent<Image>();
        buoyancy = GetComponent<BuoyantObject>();
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
        moveSpeed = 0;
        boostMeter = maxBoostDuration;
        fillMeter = false;
    }

    private void OnEnable()
    {
        move = playerControls.Boat.Move;
        gas = playerControls.Boat.Gas;
        reverse = playerControls.Boat.Reverse;
        boost = playerControls.Boat.Boost;
        AllowMovement();
        Events.startBoat.AddListener(AllowMovement);
        Events.stopBoat.AddListener(DisallowMovement);
    }

    private void OnDisable()
    {
        DisallowMovement();
        Events.startBoat.RemoveListener(AllowMovement);
        Events.stopBoat.RemoveListener(DisallowMovement);
    }

    void Start()
    {
        boatSound = FMODUnity.RuntimeManager.CreateInstance(boatSoundEvent);
        boatWaterSound = FMODUnity.RuntimeManager.CreateInstance(boatWaterSoundEvent);
        boatSound.start();
        boatWaterSound.start();
        DisallowMovement();
    }
    
    void Update()
    {
        boatSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rb));
        boatWaterSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rb));
        
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
        float boatSpeed = moveSpeed * 10 / maxSpeed;
        if (boatSpeed > 15)
        {
            boatSpeed = 15;
        }
        boatSound.setParameterByName("Speed", boatSpeed);
        boatWaterSound.setParameterByName("Speed", boatSpeed);
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
        reverse.Enable();
        boost.Enable();
    }

    public void DisallowMovement()
    {
        move.Disable();
        gas.Disable();
        reverse.Disable();
        boost.Disable();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ramp") return;
        moveSpeed = rb.velocity.magnitude;
    }
}

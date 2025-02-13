using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BuoyantObject))]
public class BoatMovement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] [Tooltip("Always slows down until at this value when not pressing the gas")] 
    private float baseMoveSpeed = 10;
    
    private float moveSpeed;
    [SerializeField, Range(1, 200)] [Tooltip("Acceleration")]
    private float acceleration = 100;
    
    [SerializeField, Range(0f, 100f), Tooltip("Rotationspeed")] private float rotationSpeed = 1f;
    [SerializeField, Range(0f, 2500f)] private float maxSpeed = 2000f;
    [SerializeField, Range(0, 90)] private int sideTiltAngle = 25;
    [SerializeField, Range(0, 90), Tooltip("How much the boat should tilt upwards at higher speeds")] private int frontTiltAngle = 25;
    [SerializeField, Range(0f, 10f)] private float tiltSpeed = 1f;
    public FMODUnity.EventReference boatSoundEvent;
    
    private FMOD.Studio.EventInstance boatSound;
    private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction gas;
    private InputAction look;
    private int moveDirection;
    private Rigidbody rb;
    
    
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        moveSpeed = baseMoveSpeed;
    }

    private void OnEnable()
    {
        move = playerControls.Boat.Move;
        move.Enable();
        gas = playerControls.Boat.Gas;
        gas.Enable();
        look = playerControls.Boat.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        gas.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boatSound = FMODUnity.RuntimeManager.CreateInstance(boatSoundEvent);
        boatSound.start();
    }
    
    void Update()
    {
        boatSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rb));
        
        if (gas.inProgress)
        {
            moveSpeed += acceleration * gas.ReadValue<float>() * Time.deltaTime;
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        else
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < baseMoveSpeed) moveSpeed = baseMoveSpeed;
        }
        float boatSpeed = moveSpeed * 15 / maxSpeed;
        boatSound.setParameterByName("Boat Engine", boatSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 euler = transform.localEulerAngles;

        float targetAngle = moveSpeed * sideTiltAngle / maxSpeed;
        float targetRot = -targetAngle * move.ReadValue<Vector2>().x;
        euler.x = Mathf.LerpAngle(euler.x, targetRot, tiltSpeed*Time.deltaTime);

        float targetRotationSpeed = moveSpeed * rotationSpeed / maxSpeed;
        euler.y += targetRotationSpeed * move.ReadValue<Vector2>().x;
        
        float targetAngleZ = moveSpeed * frontTiltAngle / maxSpeed;
        euler.z = Mathf.LerpAngle(euler.z, targetAngleZ, tiltSpeed*Time.deltaTime);
        
        rb.rotation = Quaternion.Euler(euler);
        
        if (moveSpeed == 0) return;
        rb.velocity = new Vector3(
             moveSpeed * transform.right.x, rb.velocity.y,
             moveSpeed * transform.right.z);
    }
}

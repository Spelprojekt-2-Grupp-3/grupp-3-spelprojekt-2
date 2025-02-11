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
    [SerializeField, Range(0, 90)] private int tiltAngle = 25;
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
        float boatSpeed = moveSpeed * 15 / maxSpeed;
        boatSound.setParameterByName("Boat Engine", boatSpeed);
        if (!move.inProgress && !gas.inProgress && moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < baseMoveSpeed) moveSpeed = baseMoveSpeed;
        }
        
        if (gas.inProgress)
        {
            moveSpeed += acceleration * Time.deltaTime;
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 euler = transform.localEulerAngles;

        float targetRot = -tiltAngle * look.ReadValue<Vector2>().x;
        euler.x = Mathf.LerpAngle(euler.x, targetRot, tiltSpeed*Time.deltaTime);
        
        euler.y += rotationSpeed * look.ReadValue<Vector2>().x;
        rb.rotation = Quaternion.Euler(euler);
        
        if (!move.inProgress && !gas.inProgress) return;
        rb.velocity = new Vector3(
             move.ReadValue<Vector2>().y * moveSpeed * transform.right.x, rb.velocity.y,
             move.ReadValue<Vector2>().y * moveSpeed * transform.right.z);
    }
}

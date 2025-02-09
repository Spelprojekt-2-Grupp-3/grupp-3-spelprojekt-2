using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BuoyantObject))]
public class BoatMovement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] [Tooltip("Always slows down until at this value when not pressing the gas")] 
    private float baseMoveSpeed = 10;
    private float moveSpeed;
    [SerializeField, Range(1, 200)] [Tooltip("Acceleration")]
    private float acceleration = 100;
    [SerializeField, Range(0f, 360f), Tooltip("Angles for the rotation intervals")] private float rotationSpeed = 1f;
    [SerializeField, Range(0f, 2500f)] private float maxSpeed = 2000f;
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
    }
    
    void Update()
    {
        if (!move.inProgress && !gas.inProgress) return;
        if (gas.inProgress)
        {
            moveSpeed += acceleration * Time.deltaTime;
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        else if (moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < baseMoveSpeed) moveSpeed = baseMoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        Quaternion q = Quaternion.AngleAxis(rotationSpeed, new Vector3(0, look.ReadValue<Vector2>().x, 0));
        Quaternion targetRot = rb.rotation * q;
        rb.MoveRotation(targetRot);
        if (!move.inProgress && !gas.inProgress) return;
        rb.velocity = new Vector3(
            move.ReadValue<Vector2>().y * moveSpeed * transform.right.x, rb.velocity.y,
            move.ReadValue<Vector2>().y * moveSpeed * transform.right.z);
    }
}

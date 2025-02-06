using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class BoatBoatMan : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    [Tooltip("Always slows down until at this value when not pressing the gas")]
    private float baseMoveSpeed = 10;
    private float moveSpeed;

    [SerializeField, Range(1, 200)]
    [Tooltip("Acceleration")]
    private float acceleration = 100;

    [SerializeField, Range(0f, 1f), Tooltip("Speed for the slerp of the rotation")]
    private float rotationSpeed = 0.01f;
    private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction gas;
    private Vector3 moveDirection = new Vector3();
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
        if (!move.inProgress && !gas.inProgress)
            return;
        if (gas.inProgress)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }
        else if (moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < baseMoveSpeed)
                moveSpeed = baseMoveSpeed;
        }

        moveDirection = move.ReadValue<Vector2>();
        moveDirection.Normalize();
    }

    private void FixedUpdate()
    {
        if (!move.inProgress && !gas.inProgress)
            return;
        rb.velocity = new Vector3(
            moveDirection.x * moveSpeed,
            rb.velocity.y,
            moveDirection.y * moveSpeed
        );
        if (moveDirection.magnitude > 0.0f) // Roterar inte om man inte kollar åt nån riktning
        {
            rb.rotation = Quaternion.Slerp(
                rb.rotation,
                Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y)),
                rotationSpeed
            );
        }
    }
}

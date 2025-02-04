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
    [SerializeField] [Range(0, 2000)] [Tooltip("Always slows down until at this value when not pressing the gas")] 
    private float baseMoveSpeed = 1000;
    private float moveSpeed;
    [SerializeField] [Range(100, 2000)] [Tooltip("Acceleration")]
    private float acceleration = 100;
    private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction gas;
    private Vector3 moveDirection = new Vector3();
    private Rigidbody rb;
    private bool allowMovement;
    
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        allowMovement = false;
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
        if (!move.inProgress) return;
        if (gas.inProgress)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }
        else if (moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < baseMoveSpeed) moveSpeed = baseMoveSpeed;
        }
        
        moveDirection = move.ReadValue<Vector2>();
        rb.velocity = new Vector3(
            moveDirection.x * moveSpeed * Time.deltaTime,
            rb.velocity.y,
            moveDirection.y * moveSpeed * Time.deltaTime);
        if (moveDirection.magnitude > 0.0f) // Roterar inte om man inte kollar åt nån riktning
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y)), 0.01f);
        }
    }
}

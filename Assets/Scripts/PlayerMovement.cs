using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction interact;
    [SerializeField] [Range(0, 2000)] private float moveSpeed = 100;
    private Vector3 moveDirection = new Vector3();
    private Rigidbody rb;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        move.Disable();
        interact.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        rb.velocity = new Vector3(
            moveDirection.x * moveSpeed * Time.deltaTime,
            rb.velocity.y,
            moveDirection.y * moveSpeed * Time.deltaTime);
        rb.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("interacted");
    }
}

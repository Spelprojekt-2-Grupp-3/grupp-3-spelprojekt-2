using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 0;
    private PlayerInputActions playerControls;
    private InputAction move;
    [SerializeField, Range(0, 2000)] float maxMoveSpeed;
    [SerializeField, Range(0, 300)] private float acceleration;
    private InputAction interact;
    private Rigidbody rb;
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        playerInput.onControlsChanged += ChangeDevice;
    }

    private void OnDisable()
    {
        move.Disable();
        interact.performed -= Interact;
        interact.Disable();
        playerInput.onControlsChanged -= ChangeDevice;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //If there is an active dialogue, freeze the player
        //if (DialogueManager.GetInstance().dialogueIsPlaying)
        //{
        //    return;
        //}
        
        if (!move.inProgress) return;
        if (moveSpeed < maxMoveSpeed)
        {
            moveSpeed += acceleration * move.ReadValue<Vector2>().y;
            if (moveSpeed > maxMoveSpeed)
            {
                moveSpeed = maxMoveSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 euler = transform.localEulerAngles;
        
        float targetRotationSpeed = 10f;
        euler.y += targetRotationSpeed * move.ReadValue<Vector2>().x;
        rb.rotation = Quaternion.Euler(euler);
        
        if (moveSpeed == 0) return;
        rb.velocity = new Vector3(
            moveSpeed * transform.forward.x, rb.velocity.y,
            moveSpeed * transform.forward.z);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("interacted");
    }
    
    private void ChangeDevice(PlayerInput input)
    {
        Events.checkInputEvent?.Invoke(input);
    }
}

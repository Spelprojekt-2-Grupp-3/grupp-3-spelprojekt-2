using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    private Animator _aniControl;
    private float moveSpeed = 0;
    private PlayerInputActions playerControls;
    private InputAction move, moveCam;

    [SerializeField, Range(0, 2000)]
    float maxMoveSpeed;

    [SerializeField, Range(0, 300)]
    private float acceleration;
    private InputAction interact;
    private Rigidbody rb;
    private Camera camera;
    private Vector3 currentForward;
    private Vector3 currentRight;

    private void Awake()
    {
        _aniControl = GetComponent<Animator>();
        playerControls = new PlayerInputActions();
        camera = Camera.main.GetComponent<Camera>();
        currentForward = camera.transform.forward;
        currentRight = camera.transform.right;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        moveCam = playerControls.Player.Look;
        moveCam.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        interact.performed -= Interact;
        interact.Disable();
        moveCam.Disable();
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

        /*if (!move.inProgress)
            return;*/
        Vector2 movementVector = move.ReadValue<Vector2>();
        if (moveCam.IsInProgress())
        {
            currentForward = camera.transform.forward;
            currentRight = camera.transform.right;
        }
        /* if (moveSpeed < maxMoveSpeed)
         {
             moveSpeed += acceleration * move.ReadValue<Vector2>().y;
             if (moveSpeed > maxMoveSpeed)
             {
                 moveSpeed = maxMoveSpeed;
             }
         }*/
    }

    private void FixedUpdate()
    {
         float targetRotationSpeed = 10f;
         Vector3 moveDirection = (currentForward * move.ReadValue<Vector2>().y) + (currentRight * move.ReadValue<Vector2>().x);

         if (moveDirection.sqrMagnitude > 0.01f)
         {
             Vector3 euler = transform.localEulerAngles;
             float targetYRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
             rb.rotation =  Quaternion.Euler(0, targetYRotation, 0);
         
             _aniControl.SetFloat("X", euler.x);
             _aniControl.SetFloat("Y", euler.y);
         }
         else
         {
             _aniControl.SetFloat("X", 0);
             _aniControl.SetFloat("Y", 0);
         }
         
         if (moveSpeed == 0)
             return;
         /*
         rb.velocity = new Vector3(
             1 * transform.forward.x,
             rb.velocity.y,
             1* transform.forward.z
         );
         */
    }

    private void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("interacted");
    }
}

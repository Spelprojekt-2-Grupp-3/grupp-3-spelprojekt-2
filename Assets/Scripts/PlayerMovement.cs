using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    private Animator _aniControl;
    private float moveSpeed = 0;
    private PlayerInputActions playerControls;
    private InputAction move,
        moveCam;

    [SerializeField, Range(0, 1f)]
    private float rotationLerpSpeed;

    [SerializeField, Range(0, 2000)]
    float maxMoveSpeed;

    [SerializeField, Range(0, 300)]
    private float acceleration;
    private InputAction interact;
    private Rigidbody rb;
    private Camera camera;
    private Vector3 currentForward;
    private Vector3 currentRight;
    private bool movePlayer;

    [SerializeField]
    [Range(0, 1)]
    private float _idleInterpolationSpeed;

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
        movePlayer = true;
        move = playerControls.Player.Move;
        move.Enable();
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        moveCam = playerControls.Player.Look;
        moveCam.Enable();
        Events.startPlayer.AddListener(StartPlayer);
        Events.stopPlayer.AddListener(StopPlayer);
    }

    private void OnDisable()
    {
        move.Disable();
        interact.performed -= Interact;
        interact.Disable();
        moveCam.Disable();
        Events.startPlayer.RemoveListener(StartPlayer);
        Events.stopPlayer.RemoveListener(StopPlayer);
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

        currentForward = camera.transform.forward;
        currentRight = camera.transform.right;
    }

    private void FixedUpdate()
    {
        if (!movePlayer)
            return;
        float targetRotationSpeed = 10f;
        Vector3 moveDirection =
            (currentForward * move.ReadValue<Vector2>().y)
            + (currentRight * move.ReadValue<Vector2>().x);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 euler = transform.localEulerAngles;
            float targetYRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.Slerp(
                rb.rotation,
                Quaternion.Euler(0, targetYRotation, 0),
                rotationLerpSpeed
            );

            _aniControl.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
        else
        {
//            Debug.Log(moveDirection);
            moveDirection = Vector2.Lerp(moveDirection, new Vector2(0, 0), _idleInterpolationSpeed);
            _aniControl.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        if (moveSpeed == 0)
            return;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("interacted");
    }

    private void StartPlayer()
    {
        movePlayer = true;
        move.Enable();
        moveCam.Enable();
        interact.Enable();
    }

    private void StopPlayer()
    {
        movePlayer = false;
        _aniControl.SetFloat("Speed", 0);
        Debug.Log("stopped player");
        move.Disable();
        moveCam.Disable();
        interact.Disable();
    }
}

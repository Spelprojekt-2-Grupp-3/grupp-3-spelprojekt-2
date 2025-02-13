using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationDebugger : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction move;
    [SerializeField] private Animator anim;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        if (move.ReadValue<Vector2>().y != 0) // move.ReadValue tar din moveinput och y värdet blir W och S och X värdet blir A och D
        {
            anim.SetBool("Grabbers", true);
        }
        else if(move.ReadValue<Vector2>().x != 0)
        {
            anim.SetBool("Grabbers", false);
        }
    }
}

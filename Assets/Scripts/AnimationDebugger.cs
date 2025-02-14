using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class AnimationDebugger : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction _inputDebug;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject TentacleBoatReference;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputDebug = playerControls.Player.Debug;
        _inputDebug.Enable();
        _inputDebug.performed += _ => DebugAction();
    }

    private void OnDisable()
    {
        _inputDebug.Disable();
    }

    public void OnActivation()
    {
        transform.position += Vector3.Lerp(
            transform.position,
            (TentacleBoatReference.transform.position - transform.position).normalized,
            1f
        );
        StartCoroutine(Timer(1));
    }

    private IEnumerator Timer(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Waited");
    }

    private void DebugAction()
    {
        Debug.Log("fucker");
        //anim.SetBool("Grabbers", true);
        OnActivation();
    }
    /*private void Update()
    {
        if (_inputDebug.IsPressed()) // move.ReadValue tar din moveinput och y värdet blir W och S och X värdet blir A och D
        {
            Debug.Log("fucker");
            //anim.SetBool("Grabbers", true);
            OnActivation();
        }
    }*/
}

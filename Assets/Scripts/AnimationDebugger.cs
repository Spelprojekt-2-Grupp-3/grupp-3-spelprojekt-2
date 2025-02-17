using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject _tentacleBoatReference;

    public GameObject player;

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
        Debug.Log("activating");
        playerControls.Boat.Move.Disable();
        StartCoroutine(Timer(1, Testingers));
    }

    private void Testingers()
    {
        Debug.Log("Testingers running man");
        anim.SetBool("Grabbers", true);
    }

    private bool _grabbing;

    private IEnumerator Timer(float t, Action exec)
    {
        _grabbing = true;
        StartCoroutine(test());
        yield return new WaitForSeconds(t);
        transform.LookAt(player.transform);
        //Debug.Log("Waited");
        exec();
    }

    private bool fuckerinioRonaldinio = false;

    private IEnumerator test()
    {
        fuckerinioRonaldinio = true;
        yield return new WaitForSeconds(1);
        fuckerinioRonaldinio = false;
    }

    private void DebugAction()
    {
        // Debug.Log("fucker");
        anim.SetBool("Grabbers", true);
        OnActivation();
    }

    private void Update()
    {
        if (_grabbing && fuckerinioRonaldinio)
        {
            transform.position += Vector3.Lerp(
                transform.position,
                (_tentacleBoatReference.transform.position - transform.position).normalized,
                1f
            );
        }
        /*else if (_grabbing)
        {
            transform.position = _tentacleBoatReference.transform.position;
        }*/
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KrakenLocation : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction _inputDebug;

    [SerializeField]
    private GameObject _tentacleBoatReference;

    public GameObject player;

    private bool moving = false;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    /* private void OnEnable()
     {
         _inputDebug = playerControls.Player.Debug;
         _inputDebug.Enable();
         _inputDebug.performed += _ => OnActivation();
     }*/

    /* private void OnDisable()
     {
         _inputDebug.Disable();
     }*/

    void Update()
    {
        if (moving) //this hombrero sombrero doth not work MEGALUL
        {
            Vector3 dir = (
                transform.position - _tentacleBoatReference.transform.position
            ).normalized;
            transform.Translate(dir * 20f * Time.deltaTime);

            if (
                Vector3.Distance(transform.position, _tentacleBoatReference.transform.position)
                <= 0.1f
            )
            {
                moving = false;
            }
        }
    }

    public void OnActivation() //just call to start shit
    {
        Debug.Log("activating");
        playerControls.Boat.Move.Disable();
        StartCoroutine(Timer(1, SpawnTent));
        moving = true;
    }

    private IEnumerator Timer(float t, Action exec) // so we can wait a bit for hte bastard to move before we spawn actual tentacle
    {
        yield return new WaitForSeconds(t);
        //transform.LookAt(player.transform); We do not want here
        //Debug.Log("Waited");
        exec();
    }

    private void SpawnTent()
    {
        Debug.Log("Spawning");
    }
}

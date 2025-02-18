using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class KrakenLocation : MonoBehaviour
{
    [SerializeField]
    private GameObject kraken;
    private PlayerInputActions playerControls;
    private InputAction _inputDebug;

    [SerializeField]
    private GameObject _tentacleRefLeft;

    [SerializeField]
    private GameObject _tentacleRefRight;

    public GameObject player;

    private bool moving = false;

    private enum _sideEnum
    {
        Left,
        Right
    }

    private _sideEnum side;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        side = _sideEnum.Left;
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
        playerControls.Disable();
    }*/

    void Update()
    {
        if (moving) //this hombrero sombrero doth not work MEGALUL
        {
            Vector3 dir = (_tentacleRefLeft.transform.position - transform.position).normalized;
            //  transform.position += (dir * 20f * Time.deltaTime);
            transform.Translate(dir * 20 * Time.deltaTime, Space.World);
            Debug.DrawRay(transform.position, dir, Color.red);
            if (Vector3.Distance(transform.position, _tentacleRefLeft.transform.position) <= 00.1f)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                moving = false;
                StartCoroutine(Timer(1, SpawnTent));
            }
        }
    }

    public void OnActivation() //just call to start shit
    {
        Debug.Log("SHOULD set buoyancy false");
        gameObject.GetComponent<BuoyantObject>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        Debug.Log("activating");
        Events.stopBoat?.Invoke();
        // StartCoroutine(Timer(1, SpawnTent));
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
        GameObject SpawnedKraken = Instantiate(kraken, transform.position, Quaternion.identity);
        if (side == _sideEnum.Left)
        {
            SpawnedKraken.transform.LookAt(_tentacleRefRight.transform);
        }
        else
        {
            SpawnedKraken.transform.LookAt(_tentacleRefLeft.transform);
        }
        Debug.Log("Spawning");
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class KrakenLocation : MonoBehaviour
{
    public GameObject player;

    private bool moving = false;

    public GameObject krakenMinigame;

    void Update()
    {
        if (moving) //this hombrero sombrero doth not work MEGALUL
        {
            float e = Vector3.Distance(transform.position, player.transform.position);
            float woah = 1 + (e / 10);
            Vector3 dir = (player.transform.position - transform.position).normalized;
            //  transform.position += (dir * 20f * Time.deltaTime);
            transform.Translate(dir * 20 * woah * Time.deltaTime, Space.World);
            Debug.DrawRay(transform.position, dir, Color.red);
            if (Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
            {
                moving = false;
                StartCoroutine(Timer(0.5f, SpawnTent));
            }
        }
    }

    public void OnActivation() //just call to start shit
    {
        Debug.Log("SHOULD set buoyancy false");
        gameObject.GetComponent<BuoyantObject>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        Events.stopBoat?.Invoke();

        Debug.Log("activating");
        //    Events.stopBoat?.Invoke();
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
        var k = Instantiate(krakenMinigame);
        k.GetComponent<KrakenMinigame>().StartMinigame();
        gameObject.SetActive(false);
    }
}

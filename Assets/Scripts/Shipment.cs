using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Interface!
public class Shipment : MonoBehaviour, IClick
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void OnClick()
    {
        Debug.Log("You have picked up a shipment!");
        _player.shipmentCount++;
        Destroy(gameObject);
    }
}

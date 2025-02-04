using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Interface!
public class Shipment : MonoBehaviour, IClick
{
    private Player _player;
    [SerializeField] private float weight;
    [SerializeField, Tooltip("Size that the package takes up in the inventory")] 
    private Vector2Int gridSize;
    [Tooltip("Image for the package in the 2D inventory")]
    public Image inventoryImage;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void OnClick()
    {
        Debug.Log("You have picked up a shipment!");
        _player.shipmentCount++;
        _player.shipments.Add(GetComponent<Shipment>());
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Interface!
//The 3D Item
public class Shipment : MonoBehaviour, IClick
{
    private Player _player;
    private InventoryController inventoryController;

    [Tooltip("The information about the package")]
    public PackageData packageData;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        inventoryController = FindObjectOfType<InventoryController>();
    }

    public void OnClick()
    {
        Pickup();
    }

    public void Pickup()
    {
        if (inventoryController.InsertItem(packageData))
        {
            Debug.Log("You have picked up a shipment!");
            _player.shipmentCount++;
            _player.shipments.Add(GetComponent<Shipment>());
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }
}

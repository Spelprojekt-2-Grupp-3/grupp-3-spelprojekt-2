using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    private string recipient;
    private List<string> otherRecipients;

    private InventoryController inventoryController;
    private Player player;

    [SerializeField] private Shipment shipment;

    private void Awake()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Delivery();
        }
    }

    void Pickup()
    {
        shipment.Pickup();
    }

    void Delivery()
    {
        foreach (var shipment in player.shipments)
        {
            if (shipment.packageData.recipient == recipient)
            {
                player.shipments.Remove(shipment);
            }
        }
        inventoryController.mainGrid.RemoveItemsByRecipient(recipient);
    }
}

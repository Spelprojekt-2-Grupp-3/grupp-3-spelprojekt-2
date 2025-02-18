using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] private string Inhabitant;
    [SerializeField] private IslandersListData islanders;
    private List<string> otherRecipients = new List<string>();

    private InventoryController inventoryController;
    private Player player;

    [SerializeField, Tooltip("quest shipment or smth dunno yet")] private Shipment shipment;

    private void Awake()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        player = FindObjectOfType<Player>();
        foreach (var islander in islanders.islanders)
        {
            if (islander != Inhabitant)
            {
                otherRecipients.Add(islander);
            }
        }
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
            if (shipment.packageData.recipient == Inhabitant)
            {
                player.shipments.Remove(shipment);
            }
        }
        inventoryController.mainGrid.RemoveItemsByRecipient(Inhabitant);
    }
}

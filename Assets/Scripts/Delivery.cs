using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private string recipient;
    private InventoryController inventoryController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
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
}

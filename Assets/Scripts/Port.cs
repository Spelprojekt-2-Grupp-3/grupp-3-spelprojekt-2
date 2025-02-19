using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Port : MonoBehaviour
{
    [SerializeField] private string Inhabitant;
    [SerializeField] private IslandersListData islanders;
    private List<string> otherRecipients = new List<string>();

    private PlayerInputActions inputActions;
    private InputAction interact;
    
    private InventoryController inventoryController;
    private Player player;

    [SerializeField, Tooltip("quest shipment or smth dunno yet")] private Shipment shipment;

    private bool hasBeenPickedUp;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
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

    private void OnEnable()
    {
        interact = inputActions.Player.Interact;
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoatMovement>())
        {
            Delivery();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (interact.WasPressedThisFrame() && other.GetComponent<BoatMovement>() && !hasBeenPickedUp && shipment!=null)
        {
            Pickup();
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

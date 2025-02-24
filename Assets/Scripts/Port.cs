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

    //[SerializeField, Tooltip("quest shipment or smth dunno yet")] private Shipment shipment;
    public InventoryItem item;

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

    private void OnTriggerStay(Collider other)
    {
        if (interact.WasPressedThisFrame() && other.GetComponent<Player>() && !hasBeenPickedUp && item!=null)
        {
            Pickup();
        }
        else if (other.GetComponent<Player>())
        {
            Delivery();
        }
    }

    void Pickup()
    {
        hasBeenPickedUp = true;
        item.OnPickup();
        //shipment.Pickup();
        Debug.Log("added package");
    }

    void Delivery()
    {
        for (int i = 0; i < player.items.Count; i++)
        {
            foreach (var item in player.items)
            {
                if (item.packageData.recipient == Inhabitant)
                {
                    player.items.Remove(item);
                    inventoryController.mainGrid.RemoveItem(item);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BasicDelivery : MonoBehaviour
{
    [SerializeField]
    private Player p;

    void OnTriggerEnter()
    {
        if (p.shipmentCount > 0)
        {
            p.shipmentCount--;
            Debug.Log("Completed Shipment");
        }
    }
}

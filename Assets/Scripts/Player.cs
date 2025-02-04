using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int shipmentCount = 0;
    [HideInInspector] public List<Shipment> shipments = new List<Shipment>();
}

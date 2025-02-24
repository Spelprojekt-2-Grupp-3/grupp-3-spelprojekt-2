using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public int shipmentCount = 0;
    [HideInInspector] public List<InventoryItem> items = new List<InventoryItem>();
}

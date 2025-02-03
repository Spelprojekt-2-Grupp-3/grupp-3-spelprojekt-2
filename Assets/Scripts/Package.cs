using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    [SerializeField] private float weight;
    [SerializeField, Tooltip("Size that the package takes up in the inventory")] 
    private Vector2Int gridSize;
    [Tooltip("Image for the package in the 2D inventory")]
    public Image inventoryImage;
}

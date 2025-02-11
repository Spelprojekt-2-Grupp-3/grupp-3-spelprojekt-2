using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Package Object",order = 0, fileName = "New Package Object")]
public class PackageData : ScriptableObject
{
    [Tooltip("Size that the package takes up in the inventory")] 
    public Vector2Int gridSize;
    [SerializeField] private float weight;
    [Tooltip("Sprite for the package in the 2D inventory")]
    public Sprite inventoryImage;
    [Tooltip("Who the package belongs to")]
    public string recipient;

}

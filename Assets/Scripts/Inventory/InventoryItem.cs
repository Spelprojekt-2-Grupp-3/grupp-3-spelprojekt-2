using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public PackageData packageData;

    [HideInInspector] public Vector2Int onGridPosition;

    internal void Set(PackageData data)
    {
        packageData = data;
        GetComponent<Image>().sprite = data.inventoryImage;

        Vector2 size = new Vector2();
        size.x = data.gridSize.x * ItemGrid.tileSizeWidth;
        size.y = data.gridSize.y * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
}

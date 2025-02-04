using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;
    
    [SerializeField] private int inventorySizeX;
    [SerializeField]private int inventorySizeY;

    [SerializeField] private GameObject itemPrefab;
    
    RectTransform rectTransform;
    private Vector2 positionOnGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    private Shipment[,] packageSlot;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(inventorySizeX, inventorySizeY);

        Shipment item = Instantiate(itemPrefab).GetComponent<Shipment>();
        PlaceItem(item,5, 2);
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnGrid.y / tileSizeHeight);
        return tileGridPosition;
    }

    /// <summary>
    /// Initializes Inventory grid
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    void Init(int width, int height)
    {
        packageSlot = new Shipment[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    public void PlaceItem(Shipment package, int xPos, int yPos)
    {
        RectTransform itemRectTransform = package.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);
        packageSlot[xPos, yPos] = package;

        Vector2 itemPosition = new Vector2();
        itemPosition.x = xPos * tileSizeWidth + tileSizeWidth / 2;
        //inverted y calc because grid goes from Top left instead of unity's Bottom left
        itemPosition.y = -(yPos * tileSizeHeight + tileSizeHeight / 2);
        itemRectTransform.localPosition = itemPosition;
    }

    public Shipment PickUpItem(int xPos, int yPos)
    {
        Shipment toReturn = packageSlot[xPos,yPos];
        packageSlot[xPos, yPos] = null;
        return toReturn;
    }
}

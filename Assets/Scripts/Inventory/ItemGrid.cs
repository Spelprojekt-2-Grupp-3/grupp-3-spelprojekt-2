using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;
    
    [SerializeField] private int inventorySizeX;
    [SerializeField] private int inventorySizeY;

    [SerializeField] private GameObject itemPrefab;
    
    RectTransform rectTransform;
    private Vector2 positionOnGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    private InventoryItem[,] packageSlot;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(inventorySizeX, inventorySizeY);

        InventoryItem item = Instantiate(itemPrefab).GetComponent<InventoryItem>();
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
    /// Initializes Inventory grid with given sizes
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    void Init(int width, int height)
    {
        packageSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    public void PlaceItem(InventoryItem package, int xPos, int yPos)
    {
        RectTransform itemRectTransform = package.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);

        for (int x = 0; x < package.packageData.gridSize.x; x++)
        {
            for (int y = 0; y < package.packageData.gridSize.y; y++)
            {
                packageSlot[xPos+ x, yPos + y] = package;
            }
        }

        package.onGridPosition.x = xPos;
        package.onGridPosition.y = yPos;
        
        Vector2 itemPosition = new Vector2();
        itemPosition.x = xPos * tileSizeWidth + tileSizeWidth * package.packageData.gridSize.x / 2;
        //inverted y calc because grid goes from Top left instead of unity's Bottom left
        itemPosition.y = -(yPos * tileSizeHeight + tileSizeHeight * package.packageData.gridSize.y  / 2);
        
        itemRectTransform.localPosition = itemPosition;
    }

    public InventoryItem PickUpItem(int xPos, int yPos)
    {
        InventoryItem toReturn = packageSlot[xPos,yPos];

        if (toReturn == null)
        { return null;}
        
        for (int x = 0; x < toReturn.packageData.gridSize.x; x++)
        {
            for (int y = 0; y < toReturn.packageData.gridSize.y; y++)
            {
                packageSlot[toReturn.onGridPosition.x + x, toReturn.onGridPosition.y+y] = null;
            }
        }
        
        return toReturn;
    }
}

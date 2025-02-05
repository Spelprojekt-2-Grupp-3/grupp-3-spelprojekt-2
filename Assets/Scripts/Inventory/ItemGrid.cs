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
        
        //Creates an object directly on grid. Prefab requires Data beforehand
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

    public bool PlaceItem(InventoryItem package, int xPos, int yPos)
    {
        if (CheckBoundry(xPos,yPos,package.packageData.gridSize.x, package.packageData.gridSize.y) == false)
        {
            return false;
        }

        if (CheckOverlap(xPos, yPos, package.packageData.gridSize.x, package.packageData.gridSize.y) == false)
        {
            return false;
        }
        
        
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
        
        return true;
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

    bool CheckPosition(int xPos, int yPos)
    {
        if (xPos < 0 || yPos < 0)
        {
            return false;
        }
        if (xPos >= inventorySizeX || yPos >= inventorySizeY)
        {
            return false;
        }

        return true;
    }

    bool CheckBoundry(int xPos, int yPos, int width, int height)
    {
        if (CheckPosition(xPos, yPos) == false)
            return false;

        xPos += width-1;
        yPos += height-1;
        
        if (CheckPosition(xPos, yPos) == false)
            return false;
        
        
        return true;
    }

    bool CheckOverlap(int xPos, int yPos, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (packageSlot[xPos + x, yPos + y] != null)
                {
                    /*if (overlapItem == null)
                        overlapItem = packageSlot[xPos + x, yPos + y];
                    else
                    {
                        if (overlapItem != packageSlot[xPos + x, yPos + y])
                            return false;
                    }*/
                    return false;
                }
            }
        }
        
        return true;
    }
}

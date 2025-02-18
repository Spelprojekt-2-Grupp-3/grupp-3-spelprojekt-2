using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 64;
    public const float tileSizeHeight = 64;
    
    [SerializeField] private int inventorySizeX;
    [SerializeField] private int inventorySizeY;

    RectTransform rectTransform;
    private Vector2 positionOnGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    //keeps track of the inventory slots
    private InventoryItem[,] packageSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Init(inventorySizeX, inventorySizeY);
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

    public Vector2Int FirstSlot()
    {
        int xPos = (int)(rectTransform.position.x);
        int yPos = (int)(rectTransform.position.y);
        
        return new Vector2Int(xPos,yPos);
    }

    public Vector2Int NextSlot(Vector2Int position, Vector2Int direction)
    {
        Vector2Int toReturn = position;
       
        switch (direction.x)
        {
            case 1 when position.x < rectTransform.position.x+(tileSizeWidth*inventorySizeX-tileSizeWidth):
                toReturn.x += (int)tileSizeWidth;
                break;
            case -1 when position.x > rectTransform.position.x:
                toReturn.x -= (int)tileSizeWidth;
                break;
        }

        switch (direction.y)
        {
            case 1 when position.y < rectTransform.position.y:
                toReturn.y += (int)tileSizeHeight;
                break;
            case -1 when position.y > rectTransform.position.y-(tileSizeHeight*inventorySizeY-tileSizeHeight):
                toReturn.y -= (int)tileSizeHeight;
                break;
        }

        return toReturn;
    }
    
    public bool PlaceItemWithChecks(InventoryItem package, int xPos, int yPos)
    {
        if (!CheckBoundry(xPos,yPos,package.packageData.gridSize.x, package.packageData.gridSize.y))
        {
            return false;
        }

        if (!CheckOverlap(xPos, yPos, package.packageData.gridSize.x, package.packageData.gridSize.y))
        {
            return false;
        }
        
        PlaceItem(package, xPos, yPos);

        return true;
    }

    public void PlaceItem(InventoryItem package, int xPos, int yPos)
    {
        RectTransform itemRectTransform = package.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);

        for (int x = 0; x < package.packageData.gridSize.x; x++)
        {
            for (int y = 0; y < package.packageData.gridSize.y; y++)
            {
                packageSlot[xPos + x, yPos + y] = package;
            }
        }

        package.onGridPosition.x = xPos;
        package.onGridPosition.y = yPos;

        Vector2 itemPosition = new Vector2();
        itemPosition.x = xPos * tileSizeWidth + tileSizeWidth * package.packageData.gridSize.x / 2;
        //inverted y calc because grid goes from Top left instead of unity's Bottom left
        itemPosition.y = -(yPos * tileSizeHeight + tileSizeHeight * package.packageData.gridSize.y / 2);

        itemRectTransform.localPosition = itemPosition;
    }

    public InventoryItem PickUpItem(int xPos, int yPos)
    {
        InventoryItem toReturn = packageSlot[xPos,yPos];

        if (toReturn == null)
        { return null;}
        
        RemoveItem(toReturn);
        
        return toReturn;
    }

    public void RemoveItem(InventoryItem toRemove)
    {
        for (int x = 0; x < toRemove.packageData.gridSize.x; x++)
        {
            for (int y = 0; y < toRemove.packageData.gridSize.y; y++)
            {
                packageSlot[toRemove.onGridPosition.x + x, toRemove.onGridPosition.y + y] = null;
            }
        }
    }
    
    public Vector2Int? FindSpace(InventoryItem itemToInsert)
    {
        int width = inventorySizeX - itemToInsert.packageData.gridSize.x+1;
        int height = inventorySizeY - itemToInsert.packageData.gridSize.y+1;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (CheckOverlap(x, y, itemToInsert.packageData.gridSize.x, itemToInsert.packageData.gridSize.y))
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
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
        if (!CheckPosition(xPos, yPos))
            return false;

        xPos += width-1;
        yPos += height-1;
        
        if (!CheckPosition(xPos, yPos))
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
                    //for switching held with placed in one go, incomplete
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

    public void RemoveItemsByRecipient(string recipient)
    {
        for (int x = 0; x < inventorySizeX; x++)
        {
            for (int y = 0; y < inventorySizeY; y++)
            {
                if (packageSlot[x, y] != null && packageSlot[x, y].packageData.recipient == recipient)
                {
                    RemoveItem(packageSlot[x, y]);
                }
            }
        }
    }

    public InventoryItem GetItem(int xPos, int yPos)
    {
        InventoryItem toReturn = packageSlot[xPos,yPos];

        if (toReturn == null)
        { return null;}

        return toReturn;
    }
}

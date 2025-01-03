using System;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TILESIZEWIDHT = 32;
    public const float TILESIZEHEIGHT = 32;

    private RectTransform rectTransform;

    private Vector2 positionOnTheGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    private InventoryItem[,] _inventoryItemSlot;

    [SerializeField] private int gridSizeWidth = 20;
    [SerializeField] private int gridSizeHeight = 10;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth,gridSizeHeight);
        
        
    }
    private void Init(int width , int height)
    {
        _inventoryItemSlot = new InventoryItem[width, height];
        
        Vector2 size = new Vector2(width * TILESIZEWIDHT , height * TILESIZEHEIGHT);
        rectTransform.sizeDelta = size;
    }
    
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / TILESIZEWIDHT);
        tileGridPosition.y = (int)(positionOnTheGrid.y / TILESIZEHEIGHT);


        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem , int posX , int posY,ref InventoryItem overlapItem)
    {
        if (BoundariesCheck(posX , posY , inventoryItem.itemData.width , inventoryItem.itemData.height) == false)
        {
            return false;
        }

        if (OverlapCheck(posX , posY ,inventoryItem.itemData.width , inventoryItem.itemData.height , ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }
        
        
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        
        for (int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;

        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * TILESIZEWIDHT + TILESIZEWIDHT * inventoryItem.itemData.width / 2;
        position.y = -(posY * TILESIZEHEIGHT + TILESIZEHEIGHT * inventoryItem.itemData.height / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_inventoryItemSlot[posX + x , posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = _inventoryItemSlot[posX + x , posY + y];
                    }
                    else
                    {
                        if (overlapItem != _inventoryItemSlot[posX + x , posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        
        
        return true;
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = _inventoryItemSlot[x, y];

        if (toReturn == null) return null;
        
        CleanGridReference(toReturn);
        
      
        
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int i = 0; i < item.itemData.width; i++)
        {
            for (int j = 0; j < item.itemData.height; j++)
            {
                _inventoryItemSlot[item.onGridPositionX + i, item.onGridPositionY + j] = null;
            }
        }
    }

    public bool PositionCheck(int posX , int posY)
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundariesCheck(int posX , int posY , int width , int height)
    {
        if (PositionCheck(posX,posY) == false)
        {
            return false;
        }

        posX += width - 1;
        posY += height - 1;
        
        if (PositionCheck(posX,posY) == false)
        {
            return false;
        }

        return true;
    }

    public InventoryItem GetItem(int x, int y)
    {
        return _inventoryItemSlot[x , y];
    }
}

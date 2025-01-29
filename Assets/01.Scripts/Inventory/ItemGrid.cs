using System;
using System.Text;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TILESIZEWIDHT = 64;
    public const float TILESIZEHEIGHT = 64;

    public static readonly Vector2 TILESIZE = new Vector2(TILESIZEWIDHT , TILESIZEHEIGHT);
    
    private RectTransform rectTransform;

    private Vector2 positionOnTheGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    private InventoryItem[,] _inventoryItemSlot;

    [SerializeField] private int gridSizeWidth = 20;
    [SerializeField] private int gridSizeHeight = 10;
    
    public Unit owner;
    
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

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundariesCheck(posX, posY, inventoryItem.myBoolArray.Width, inventoryItem.myBoolArray.Height) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem, ref overlapItem) == false)  // 시그니처 수정
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
    
        for (int y = 0; y < inventoryItem.myBoolArray.Height; y++)
        {
            for (int x = 0; x < inventoryItem.myBoolArray.Width; x++)
            {
                if (inventoryItem.myBoolArray.GetValue(x, y))
                    _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
    
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);
        rectTransform.localPosition = position;
    
        owner.AddStat(inventoryItem.StatSo);        
    
        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * TILESIZEWIDHT + TILESIZEWIDHT * inventoryItem.myBoolArray.Width / 2;
        position.y = -(posY * TILESIZEHEIGHT + TILESIZEHEIGHT * inventoryItem.myBoolArray.Height / 2);
        return position;
    }

    
    private bool OverlapCheck(int posX, int posY, InventoryItem item, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < item.myBoolArray.Width; x++)
        {
            for (int y = 0; y < item.myBoolArray.Height; y++)
            {
                if (item.myBoolArray.GetValue(x, y))  // 아이템의 실제 모양에서만 검사
                {
                    if (_inventoryItemSlot[posX + x, posY + y] != null)
                    {
                        if (overlapItem == null)
                        {
                            overlapItem = _inventoryItemSlot[posX + x, posY + y];
                        }
                        else
                        {
                            if (overlapItem != _inventoryItemSlot[posX + x, posY + y])
                            {
                                return false;
                            }
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
        
        //removeStatLogic
        owner.RemoveStat(toReturn.StatSo);
        
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int y = 0; y < item.myBoolArray.Height; y++)
        {
            for (int x = 0; x < item.myBoolArray.Width; x++)
            {
                if (item.myBoolArray.GetValue(x,y))
                    _inventoryItemSlot[item.onGridPositionX + x, item.onGridPositionY + y] = null;
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
    
    [ContextMenu("Log")]
    public void Log()
    {
        StringBuilder sb = new StringBuilder(); 
        for (int y = 0; y < gridSizeHeight; y++) 
        {
            for (int x = 0; x < gridSizeWidth; x++) // 열 순회
            {
                if (_inventoryItemSlot[x, y] == null)
                {
                    sb.Append("0 ");
                }
                else
                {
                    sb.Append("1 ");
                }
                
            }
            
            sb.Append("\n"); 
        }
        
        Debug.Log(sb);
    }

    
}

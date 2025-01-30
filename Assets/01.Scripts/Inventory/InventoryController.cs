using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlighter.SetParent(value);
        }
    }
    
    public InventoryItem selectItem;
    public InventoryItem overlapItem;
    private RectTransform rectTransform;

    private Vector2Int oldPositon;
    private InventoryItem itemToHighlight;
    private InventoryHighlighter inventoryHighlighter;
    private GridInteract gridInteract;
    
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;
    
    private void Awake()
    {
        inventoryHighlighter = GetComponent<InventoryHighlighter>();
        gridInteract = GetComponent<GridInteract>();
    }

    private void Update()
    {
        ItemIconDrag();
                
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
            
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(selectItem == null)return;
            
            selectItem.Rotate();
            
        }
        
        if (selectedItemGrid == null)
        {
            inventoryHighlighter.Show(false);
            return;
        }
                
        HandleHighlight();
        
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
                
        //if(positionOnGrid == oldPositon)return;
        
        oldPositon = positionOnGrid;
        if (selectItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x , positionOnGrid.y);
            
            if (itemToHighlight != null)
            {
                inventoryHighlighter.Show(true);
                inventoryHighlighter.SetSize(itemToHighlight,selectedItemGrid);
                inventoryHighlighter.SetPosition(selectedItemGrid , itemToHighlight);
            }
            else
            {
                inventoryHighlighter.Show(false);
            }
            
        }
        else
        {
            inventoryHighlighter.Show(selectedItemGrid.BoundariesCheck(positionOnGrid.x , positionOnGrid.y , 
                selectItem.myBoolArray.Width , selectItem.myBoolArray.Height));
            
            
            inventoryHighlighter.SetSize(selectItem,selectedItemGrid);
            inventoryHighlighter.SetPosition(selectedItemGrid , selectItem , positionOnGrid.x , positionOnGrid.y);
        }
    }
    
    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        
        selectItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 mousePos = Input.mousePosition;

        if (selectItem != null)
        {
            mousePos.x -= (selectItem.myBoolArray.Width - 1) * ItemGrid.TILESIZEWIDHT / 2;
            mousePos.y += (selectItem.myBoolArray.Height - 1) * ItemGrid.TILESIZEHEIGHT / 2;
        }

        return selectedItemGrid.GetTileGridPosition(mousePos);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete =selectedItemGrid.PlaceItem(selectItem, tileGridPosition.x, tileGridPosition.y,ref overlapItem);

        if (complete)
        {
            selectItem = null;
            if (overlapItem != null)
            {
                selectItem = overlapItem;
                overlapItem = null;
                rectTransform = selectItem.transform as RectTransform;
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectItem != null)
        {
            rectTransform = selectItem.GetComponent<RectTransform>();
           
        }
    }

    private void ItemIconDrag()
    {
        if (selectItem != null)
        {                   
            rectTransform.position = Input.mousePosition;
            if (selectedItemGrid != null && selectedItemGrid != transform.parent)
                selectItem.transform.SetParent(selectedItemGrid.transform);
        }
    }
    
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private InventoryController _inventoryController;
    private ItemGrid _itemGrid;

    public Vector2 startPos;
    public float openXPos;
    private RectTransform rectTransform => transform as  RectTransform;
    public float duration;
    
    public bool isOpen = false;
    
    private void Start()
    {
        _inventoryController = FindAnyObjectByType<InventoryController>();
        _itemGrid = GetComponent<ItemGrid>();

        startPos = rectTransform.anchoredPosition;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = _itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = null;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        isOpen = !isOpen;
        
        if (isOpen)
        {
            rectTransform.DOMoveX(openXPos , duration);
        }
        else
        {
            rectTransform.DOMoveX(startPos.x , duration);
        }

    }
}

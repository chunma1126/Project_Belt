using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryController _inventoryController;
    private ItemGrid _itemGrid;

    public Vector2 startPos;
    private RectTransform rectTransform => transform as  RectTransform;
    public float duration;

   
    
    private void Start()
    {
        _inventoryController = FindAnyObjectByType<InventoryController>();
        _itemGrid = GetComponent<ItemGrid>();

        //rectTransform.DOMove(startPos , duration);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = _itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = null;
    }
}

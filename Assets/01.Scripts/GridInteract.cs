using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryController _inventoryController;
    private ItemGrid _itemGrid;
    
    private void Start()
    {
        _inventoryController = FindAnyObjectByType<InventoryController>();
        _itemGrid = GetComponent<ItemGrid>();
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

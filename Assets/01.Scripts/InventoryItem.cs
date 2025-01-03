using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int onGridPositionX;
    public int onGridPositionY;
    
    public void Set(ItemData item)
    {
        itemData = item;

        GetComponent<Image>().sprite = itemData.itemIcon;
        Vector2 size = new Vector2
        {
            x = itemData.width * ItemGrid.TILESIZEWIDHT,
            y = itemData.height * ItemGrid.TILESIZEHEIGHT
        };
        GetComponent<RectTransform>().sizeDelta = size;
    }
}

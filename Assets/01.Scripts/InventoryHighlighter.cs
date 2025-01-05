using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class InventoryHighlighter : MonoBehaviour
{
    [SerializeField] private RectTransform highlighter;
    [SerializeField] private Image highlighterImage;
    
    
    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.myBoolArray.Width * ItemGrid.TILESIZEWIDHT;
        size.y = targetItem.itemData.myBoolArray.Height * ItemGrid.TILESIZEHEIGHT;

        highlighter.sizeDelta = size;

    }

    public void SetPosition(ItemGrid targetGrid , InventoryItem targetItem)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem,targetItem.onGridPositionX , targetItem.onGridPositionY);

        highlighter.localPosition = pos;
    }

    public void SetPosition(ItemGrid targetGrid , InventoryItem targetItem , int posX , int posY)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem,posX , posY);
        highlighter.localPosition = pos;
    }

    public void SetParent(ItemGrid target)
    {
        if(target != null)
            highlighter.SetParent(target.transform);
    }


    public void Show(bool b)
    {
        if (b)
        {
            highlighterImage.color = new Color(highlighterImage.color.r , highlighterImage.color.g , highlighterImage.color.b , 0.4f);
        }
        else
        {

            highlighterImage.color = new Color(highlighterImage.color.r , highlighterImage.color.g , highlighterImage.color.b , 0);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class InventoryHighlighter : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private RectTransform highlighter;

    private List<RectTransform> highlightList;
    [SerializeField] private int highlightCount;

    private void Start()
    {
        highlightList = new List<RectTransform>();
        for (int i = 0; i < highlightCount; i++)
        {
            RectTransform newHighlight = Instantiate(highlighter, root);
            newHighlight.gameObject.SetActive(false);
            newHighlight.sizeDelta = ItemGrid.TILESIZE;
            
            highlightList.Add(newHighlight);
        }
    }

    public void SetSize(InventoryItem targetItem, ItemGrid itemGrid)
    {
        MyBoolArray boolArray = targetItem.myBoolArray;
        int index = 0;
        
        for (int i = 0; i < boolArray.Width; i++)
        {
            for (int j = 0; j < boolArray.Height; j++)
            {
                if (boolArray.GetValue(i, j))
                {
                    highlightList[index].gameObject.SetActive(true);

                    /*highlightList[index].anchoredPosition =
                        new Vector2(targetItem.onGridPositionX * ItemGrid.TILESIZEWIDHT, targetItem.onGridPositionY * ItemGrid.TILESIZEHEIGHT) +
                        new Vector2(ItemGrid.TILESIZEWIDHT * i , ItemGrid.TILESIZEHEIGHT * j ) +
                        (itemGrid.transform as RectTransform).anchoredPosition;*/

                    index++;
                }
            }
        }
        
        for (int i = index; i < highlightList.Count; i++)
        {
            highlightList[i].gameObject.SetActive(false);
        }
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        MyBoolArray boolArray = targetItem.myBoolArray;
        int index = 0;
        
        Vector2 offset = CalculatorHighlightOffSet(boolArray.Width , boolArray.Height);
        
        for (int i = 0; i < boolArray.Width; i++)
        {
            for (int j = 0; j < boolArray.Height; j++)
            {
                if (boolArray.GetValue(i, j))
                {
                   
                    Vector2 pos = targetGrid.CalculatePositionOnGrid(
                        targetItem,
                        targetItem.onGridPositionX + i - 1,
                        targetItem.onGridPositionY + j - 1
                    );
                    highlightList[index].gameObject.SetActive(true);
                    highlightList[index].localPosition = pos + offset;
                }
            }
        }
        
        for (int i = index; i < highlightList.Count; i++)
        {
            highlightList[i].gameObject.SetActive(false);
        }
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)
    {
        MyBoolArray boolArray = targetItem.myBoolArray;
        int index = 0;
        
        Vector2 offset = CalculatorHighlightOffSet(boolArray.Width , boolArray.Height);
        
        for (int i = 0; i < boolArray.Width; i++)
        {
            for (int j = 0; j < boolArray.Height; j++)
            {
                if (boolArray.GetValue(i, j))
                {
                   
                    Vector2 pos = targetGrid.CalculatePositionOnGrid
                    (
                        targetItem,
                        posX + i - 1,
                        posY + j - 1
                    );
                    highlightList[index].gameObject.SetActive(true);
                    highlightList[index].localPosition = pos + offset;
                    index++;
                }
            }
        }
        
        for (int i = index; i < highlightList.Count; i++)
        {
            highlightList[i].gameObject.SetActive(false);
        }
    }

    public void SetParent(ItemGrid target)
    {
        if (target != null)
        {
            foreach (var highlight in highlightList)
            {
                highlight.SetParent(target.transform);
            }
        }
    }

    public void Show(bool b)
    {
        float alpha = b ? 0.4f : 0f;
        foreach (var highlight in highlightList)
        {
            Image image = highlight.GetComponent<Image>();
            if (image != null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            }
        }
    }

    private Vector2 CalculatorHighlightOffSet(int x , int y)
    {
        if (x == 2 && y == 2)
            return Vector2.zero;
                
        Vector2 offset = new Vector2();

        float offsetSizeWidth = ItemGrid.TILESIZEWIDHT / 2;
        float offsetSizeHeight = ItemGrid.TILESIZEHEIGHT / 2;
        
        if (x == 1)
            offset.x = -offsetSizeWidth;
        if (y == 1)
            offset.y = -offsetSizeHeight;

        int xCount = x - 2;

        for (int i = 0; i < xCount; i++)
        {
            offset.x -= offsetSizeWidth;
        }
        
        int yCount = y - 2;

        for (int i = 0; i < yCount; i++)
        {
            offset.y += offsetSizeWidth;
        }
        
        
        return offset;
    }
    
    public void SetRotation(Quaternion _rotation)
    {
        /*foreach (var highlight in highlightList)
        {
            highlight.rotation = _rotation;
        }*/
    }
}
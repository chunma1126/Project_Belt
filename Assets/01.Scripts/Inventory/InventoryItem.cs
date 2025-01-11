using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public MyBoolArray myBoolArray;

    public Sprite itemIcon;
    public int onGridPositionX;
    public int onGridPositionY;
    
    public int itemRotation;

    private void Start()
    {
        myBoolArray.LoadGridFromSerialized();
        GetComponent<Image>().sprite = itemIcon;
        Vector2 size = new Vector2
        {
            x = myBoolArray.Width * ItemGrid.TILESIZEWIDHT,
            y = myBoolArray.Height * ItemGrid.TILESIZEHEIGHT
        };
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Set()
    {
        
    }

    public void Rotate()
    {
        itemRotation += 90;
        if (itemRotation >= 360) itemRotation = 0;
        
        transform.rotation = Quaternion.Euler(0, 0, itemRotation);
        myBoolArray.RotateGrid();
    }

    public Quaternion GetRotate() => transform.rotation;
    public void Test()
    {
        for (int y = 0; y < myBoolArray.Height; y++) // 행(row)을 먼저 순회
        {
            StringBuilder sb = new StringBuilder(); // 각 줄마다 StringBuilder 초기화
            for (int x = 0; x < myBoolArray.Width; x++) // 열(column)을 순회
            {
                sb.Append(myBoolArray.GetValue(x, y) + " "); // 값을 문자열로 추가
            }
            Debug.Log(sb.ToString()); // 한 줄 출력
        }
    }
}

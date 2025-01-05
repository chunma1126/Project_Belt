using System;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemData : ScriptableObject
{
    public MyBoolArray myBoolArray;

    public string itemName;
    public Sprite itemIcon;
    
    
    [ContextMenu("Print")]
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

using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;

    public string itemName;
    public Sprite itemIcon;

    private void OnValidate()
    {
        
    }
}

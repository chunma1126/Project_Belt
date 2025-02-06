using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/Item")]
public class PoolItemSO : ScriptableObject
{
    public string poolName;
    public PoolableMono prefab;
    public int poolCount;
            
}

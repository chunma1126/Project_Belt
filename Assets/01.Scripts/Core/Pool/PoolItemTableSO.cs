using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolItemTable", menuName = "SO/Pool/Table")]
public class PoolItemTableSO : ScriptableObject
{
    public List<PoolItemSO> poolList;

    public List<PoolItemSO> GetTable() => poolList;

}

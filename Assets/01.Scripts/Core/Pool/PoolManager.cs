using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    MageProjectile,
    Explosion,
}

public class PoolManager : Singleton<PoolManager>
{
    public PoolItemTableSO poolTable;
    private Dictionary<PoolType, Pool<PoolableMono>> poolDictinay = new Dictionary<PoolType, Pool<PoolableMono>>();
    
    
    protected override void Awake()
    {
        base.Awake();
        
        foreach (var item in poolTable.poolList)
        {
            CreatePool(item);
        }
    }

    private void CreatePool(PoolItemSO item)
    {
        poolDictinay.Add(item.prefab.type , new Pool<PoolableMono>(item.prefab , transform , item.poolCount));
    }
    
    public PoolableMono Pop(PoolType type)
    {
        if(poolDictinay.ContainsKey(type) == false)
        {
            Debug.LogError($"Prefab does not exist on pool : {type.ToString()}");
            return null;
        }

        PoolableMono item = poolDictinay[type].Pop();
        item.ResetItem();
        return item;
    }

    public void Push(PoolableMono obj, bool resetParent = false)
    {
        if (resetParent)
            obj.transform.parent = transform;
        poolDictinay[obj.type].Push(obj);
    }
    
}

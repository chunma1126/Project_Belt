using UnityEngine;
using System.Collections.Generic;

public class Pool<T> where T : PoolableMono
{
    private T prefab;
    private Transform parent;
    private Queue<T> pool;
    private int initialSize;

    public Pool(T prefab, Transform parent, int initialSize = 10)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.initialSize = initialSize;
                
        Initialize();
    }

    private void Initialize()
    {
        pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewInstance();
        }
    }

    private void CreateNewInstance()
    {
        T obj = GameObject.Instantiate(prefab, parent);
        obj.Initialize();
        obj.gameObject.SetActive(false);
        
        pool.Enqueue(obj);
    }

    public T Pop()
    {
        if (pool.Count == 0)
        {
            CreateNewInstance();
        }

        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        Debug.LogWarning("Pool is empty and auto-expand is disabled!");
        return null;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void Clear()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            GameObject.Destroy(obj.gameObject);
        }
        pool.Clear();
    }
}
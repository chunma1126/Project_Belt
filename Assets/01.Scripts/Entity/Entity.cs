using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Dictionary<Type, IEntityComponent> Components;
        
    protected virtual void Awake()
    {
        Components = new Dictionary<Type, IEntityComponent>();
        GetComponentsInChildren<IEntityComponent>().ToList().ForEach(x => Components.Add(x.GetType() , x));

        foreach (IEntityComponent item in Components.Values)
        {
            item.Initialize(this);
        }
    }
    public T GetCompo<T>() where T : class
    {
        if(Components.TryGetValue(typeof(T), out IEntityComponent compo))
        {
            return compo as T;
        }
        return default;
    }

    

}

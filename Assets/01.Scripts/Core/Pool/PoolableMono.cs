using UnityEngine;

public abstract class PoolableMono : MonoBehaviour 
{
    public PoolType type;

    public abstract void Initialize();
    public abstract void ResetItem();
}

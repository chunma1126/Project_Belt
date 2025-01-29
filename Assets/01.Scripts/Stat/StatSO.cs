using System;
using UnityEngine;

[CreateAssetMenu(fileName = "_Stat", menuName = "SO/Stat")]
public class StatSO : ScriptableObject
{
    public StatType StatType;

    public float baseAmount;
    public float modifier;
    
    private event Action<float> OnChangeStatAction;
    
    [Space]
    public bool useMaxValue;
    public float maxValue;
    
    private void OnValidate()
    {
        if (useMaxValue)
        {
            if (maxValue < 0)
            {
                maxValue = 0;
            }
        }
    }

    public void Initialize()
    {
        OnChangeStatAction?.Invoke(baseAmount + modifier);
        modifier = 0;
    }
    
    public void Add(float _amount)
    {
        modifier += _amount;

        if (useMaxValue)
        {
            float currentValue = GetValue();
            if (currentValue > maxValue)
            {
                float excess = currentValue - maxValue;
                modifier -= excess;
            }
        }
        
        OnChangeStatAction?.Invoke(baseAmount + modifier);
    }
    
    public void Remove(float _amount)
    {
        modifier -= _amount;
        if (modifier < 0)
            modifier = 0;
        
        OnChangeStatAction?.Invoke(baseAmount + modifier);
    }

    public void AddStatCallback(Action<float> _callback)
    {
        OnChangeStatAction += _callback;
    }
    
    public void RemoveStatCallback(Action<float> _callback)
    {
        OnChangeStatAction -= _callback;
    }
        
    public float GetValue()
    {
        float totalValue = modifier + baseAmount;
        return totalValue;
    }
    
    public void ResetModifier()
    {
        modifier = 0;        
        OnChangeStatAction?.Invoke(baseAmount + modifier);
    }
    
   
}
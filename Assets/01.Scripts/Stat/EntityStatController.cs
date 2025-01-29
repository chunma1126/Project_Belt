using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatType
{
    Attack,//공격력
    Critical,//치명타 피해,치명타 확률은 10%로 고정
    MoveSpeed,//움직이는 속도
    AttackSpeed,//공격 속도
    Health,//체력
    ActiveSkillTime,
}

public class EntityStatController : MonoBehaviour,IEntityComponent
{
    public List<StatSO> StatList;
    private Dictionary<StatType, StatSO> statDictionary;
    
    private Entity entity;
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;
        
        statDictionary = new Dictionary<StatType, StatSO>();
        
        foreach (var item in StatList)
        {
            StatType type = item.StatType;
            statDictionary.Add(type , item);
            item.Initialize();
        }
    }

    private void Start()
    {
        foreach (var item in StatList)
        {
            item.Initialize();
        }
    }

    public float GetValue(StatType _statType)
    {
        return statDictionary[_statType].GetValue();
    }

    public StatSO GetStat(StatType _statType)
    {
        return statDictionary[_statType];
    }
    
    public void AddValue(StatType _statType , float _amount)
    {
        statDictionary[_statType].Add(_amount);
    }
    
    public void RemoveValue(StatType _statType , float _amount)
    {
        statDictionary[_statType].Remove(_amount);
    }

    
    
    
}

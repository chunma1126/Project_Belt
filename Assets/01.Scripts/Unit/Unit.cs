using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : Entity
{
    [Header("Combat info")]
    public Transform Target;
    public AnimationParamSO attackSpeedParam;
    public AnimationParamSO comboCounterParam;
    
    public float attackRadius;
    public float attackDuration;
    
    [Header("StateSO Info")]
    public List<StateSO> stateList;
    private EntityStateMachine StateMachine;

    [Header("ActiveSkill info")] 
    private float activeSkillTime;
    private float activeSkillTimer = 0;
    public UnityEvent ActiveSkillOnEvent;
    public UnityEvent ActiveSkillOffEvent;
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine(stateList , this);
        StateMachine.Initialize("IDLE");
        
        GetCompo<EntityStatController>().GetStat(StatType.AttackSpeed).AddStatCallback(HandleSetAttackSpeed);
        GetCompo<EntityStatController>().GetStat(StatType.ActiveSkillTime).AddStatCallback(HandleSetActiveSkillTime);
        
        activeSkillTime = GetCompo<EntityStatController>().GetValue(StatType.ActiveSkillTime);
    }
    
    

    protected virtual void Update()
    {
        StateMachine.CurrentStateUpdate();


        UpdateActiveSkill();
    }

    private void UpdateActiveSkill()
    {
        //전투중이 아닐때는 안흘러가야지 그거 막아야함.
        //if()
        
        activeSkillTimer += Time.deltaTime;
        if (activeSkillTimer >= activeSkillTime)
        {
            activeSkillTimer = 0;
            ActiveSkill();
        }
    }

    protected abstract void ActiveSkill();

    public void ChangeState(string _stateName)
    {
        StateMachine.ChangeState(_stateName);
    }

    public void SetTarget(Transform _trm)
    {
        Target = _trm;
    }

    private void HandleSetAttackSpeed(float _amount)   
    {
        GetCompo<EntityAnimator>().SetParam(attackSpeedParam , _amount);
    }
    
    private void HandleSetActiveSkillTime(float _amount)
    {
        activeSkillTime = _amount;
        activeSkillTimer = 0;
    }

    public void AddStat(StatSO _newStat)
    {
        GetCompo<EntityStatController>().AddValue(_newStat.StatType , _newStat.GetValue());
    }
    
    public void RemoveStat(StatSO _newStat)
    {
        GetCompo<EntityStatController>().RemoveValue(_newStat.StatType , _newStat.GetValue());
    }
        
}

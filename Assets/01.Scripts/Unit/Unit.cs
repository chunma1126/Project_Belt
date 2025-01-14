using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
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
                
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine(stateList , this);
        StateMachine.Initialize("IDLE");
        
        GetCompo<EntityStatController>().GetStat(StatType.AttackSpeed).AddStatCallback(HandleSetAttackSpeed);
    }
     
    private void Update()
    {
        StateMachine.CurrentStateUpdate();
    }

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

}

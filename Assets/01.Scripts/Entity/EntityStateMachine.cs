using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EntityStateMachine
{
    public EntityState currentState { get; private set; }

    private Dictionary<string, EntityState> _states;
    
    public EntityStateMachine(List<StateSO> states, Entity entity)
    {
        _states = new Dictionary<string, EntityState>();
        
        foreach (var item in states)
        {
            try
            {
                Type type = Type.GetType(item.className);
                            
                var entityState = Activator.CreateInstance(type,entity , item.stateAnimationParam) as EntityState;
                _states.Add(item.stateName , entityState);
                
            }
            catch (Exception e)
            {
                Debug.LogError("State is not find");
            }
        }
    }

    public void Initialize(string _stateName)
    {
        currentState = _states[_stateName];
        _states[_stateName].Enter();
    }

    public void ChangeState(string _stateName)
    {
        currentState.Exit();
        currentState = _states[_stateName];
        currentState.Enter();
    }
    
    public void CurrentStateUpdate()
    {
        currentState.Update();
    }




}

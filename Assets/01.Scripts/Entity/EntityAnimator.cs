using System;
using UnityEngine;

public class EntityAnimator : MonoBehaviour,IEntityComponent
{
    private Animator _animator;

    private Entity entity;

    public event Action OnAnimationEndEvent;
    public event Action OnAttackEvent; 
    
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;
        _animator = GetComponent<Animator>();
    }
    
    #region SetParam

    public void SetParam(AnimationParamSO param , bool value)//bool
    {
        _animator.SetBool(param.animationName , value);
    }
    
    public void SetParam(AnimationParamSO param)//trigger
    {
        _animator.SetTrigger(param.animationName );
    }
    
    public void SetParam(AnimationParamSO param , int value)//int
    {
        _animator.SetInteger(param.animationName , value);
    }
    
    public void SetParam(AnimationParamSO param , float value)//float
    {
        _animator.SetFloat(param.animationName , value);
    }
    
    #endregion

    protected virtual void AnimationEnd() => OnAnimationEndEvent?.Invoke();
    
    protected virtual void AttackTrigger() => OnAttackEvent?.Invoke();
    
    
}

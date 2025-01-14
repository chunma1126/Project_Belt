using UnityEngine;

public class KnightAttackState : EntityState
{
    private Unit knight;

    private float lastAttackTime;
    private float attackDuration;

    private int comboCount;
    
    
    public KnightAttackState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        knight = entity as Unit;
    }

    public override void Enter()
    {
        base.Enter();
        animator.OnAnimationEndEvent += TriggerCalled;
        
        attackDuration = knight.attackDuration;
        
        if (Time.time > lastAttackTime + attackDuration)
        {
            comboCount = 0;
        }
        else
        {
            comboCount++;
            
            if(comboCount > 1)
                comboCount = 0;
        }
                        
        animator.SetParam(knight.comboCounterParam , comboCount);
    }

    public override void Update()
    {
        base.Update();
        
        if (isTriggerd)
        {
            knight.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        animator.OnAnimationEndEvent -= TriggerCalled;
    }

    private void TriggerCalled()
    {
        isTriggerd = true;
    }
    
    
}
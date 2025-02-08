using UnityEngine;

public class KnightMoveState : EntityState
{
    private KnightUnit Knight;
    
    private EntityMover entityMover;

    private float attackRadius;
    
    public KnightMoveState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        Knight = entity as KnightUnit;
                
        entityMover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        attackRadius = Knight.attackRadius;
        
    }

    public override void Update()
    {
        base.Update();
        
        float distance = Vector2.Distance(Knight.transform.position , Knight.Target.position);
        
        if (distance > attackRadius)
        {
            float directionToTarget = (Knight.Target.position.x - Knight.transform.position.x);
            float lookDirection = Mathf.Clamp(directionToTarget , -1 , 1);
            
            if(entityMover.Move(lookDirection) == false)
                Knight.ChangeState("IDLE");
        }
        else
        {
            Knight.ChangeState("ATTACK");
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        
        entityMover.StopImmediately();
    }
}

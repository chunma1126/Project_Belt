using UnityEngine;

public class MageMoveState : EntityState
{
    private MageUnit mage;
    private EntityMover entityMover;
    private EntityRenderer entityRenderer;
    
    private const float MIN_DISTANCE = 1.5f;
    
    public MageMoveState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        mage = entity as MageUnit;
        entityMover = entity.GetCompo<EntityMover>();
        entityRenderer = entity.GetCompo<EntityRenderer>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float distance = Vector3.Distance(mage.transform.position, mage.Target.position);
        float directionToTarget = (mage.Target.position.x - mage.transform.position.x);
        float lookDirection = Mathf.Clamp(directionToTarget, -1, 1);
    
        if ((lookDirection < 0 && entityRenderer.lookRight) || (lookDirection > 0 && !entityRenderer.lookRight))
        {
            entityRenderer.Flip();
        }

        if (distance > mage.attackRadius)
        {
            if (entityMover.Move(lookDirection) == false)
                mage.ChangeState("IDLE");
        }
        else if (distance < mage.attackRadius - MIN_DISTANCE) 
        {
            if (entityMover.Move(-lookDirection) == false)
                mage.ChangeState("IDLE");
        }
        else 
        {  
            entityMover.Move(0);
            mage.ChangeState("ATTACK");
        }
    }


    public override void Exit()
    {
        base.Exit();
    }
}
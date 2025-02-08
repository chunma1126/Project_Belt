using UnityEngine;

public class CatMoveState : EntityState
{
    private CatUnit cat;
    private EntityMover entityMover;

    private float minDistance;
            
    public CatMoveState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        cat = entity as CatUnit;
        entityMover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        minDistance = cat.minDistance;
    }

    public override void Update()
    {
        base.Update();

        float directionToTarget = cat.Target.position.x - cat.transform.position.x;
        float lookDirection = Mathf.Clamp(directionToTarget, -1, 1);
        float distance = Mathf.Abs(directionToTarget);
        
        if (distance < minDistance)
        {
            if(entityMover.Move(-lookDirection) == false)
                cat.ChangeState("IDLE");
        }
        else
        {
            cat.ChangeState("HEAL");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
using UnityEngine;

public class KnightMoveState : EntityState
{
    private Unit Knight;
    
    private EntityMover entityMover;
    private EntityRenderer entityRenderer;
        
    public KnightMoveState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        Knight = entity as Unit;
                
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
        
        if (Input.GetKeyUp(KeyCode.P))
        {
            Knight.ChangeState("IDLE");    
        }
        
        int lookDirection = renderer.lookDirection;
        entityMover.Move(lookDirection);
        
    }

    public override void Exit()
    {
        base.Exit();
        
        entityMover.StopImmediately();
    }
}

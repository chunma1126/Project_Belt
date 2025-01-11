using UnityEngine;

public class KnightIdleState : EntityState
{
    private Unit knight;
    public KnightIdleState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        knight = entity as Unit;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.P))
            knight.ChangeState("MOVE");
        
        if(Input.GetKeyDown(KeyCode.R))
            renderer.Flip();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

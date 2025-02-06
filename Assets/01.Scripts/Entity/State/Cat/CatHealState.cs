using UnityEngine;

public class CatHealState : EntityState
{
    private CatUnit cat;
    
    public CatHealState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        cat = entity as CatUnit;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (isTriggerd)
        {
            cat.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

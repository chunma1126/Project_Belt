using UnityEngine;

public class MageAttackState : EntityState
{
    private MageUnit mage;
    public MageAttackState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        mage = entity as MageUnit;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if(isTriggerd)
            mage.ChangeState("IDLE");
    }

    public override void Exit()
    {
        base.Exit();
    }
}

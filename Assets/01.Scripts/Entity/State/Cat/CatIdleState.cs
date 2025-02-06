using UnityEngine;

public class CatIdleState : EntityState
{
    private readonly CatUnit cat;
    
    private float idleTime;
    private float timer;

    private readonly float DEFULAT_IDLE_TIME = 4;
    
    public CatIdleState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        cat = entity as CatUnit;
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = DEFULAT_IDLE_TIME;
        idleTime -= entity.GetCompo<EntityStatController>().GetValue(StatType.AttackSpeed);
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer >= idleTime)
            cat.ChangeState("MOVE");
    }

    public override void Exit()
    {
        base.Exit();
        timer = 0;
    }
}

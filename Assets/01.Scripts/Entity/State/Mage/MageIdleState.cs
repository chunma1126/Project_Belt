using UnityEngine;

public class MageIdleState: EntityState
{
    private MageUnit mage;
        
    private float idleTime;
    private float timer;
    
    private const float  DEFULAT_IDLE_TIME = 2.5f;
    
    public MageIdleState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        mage = entity as MageUnit;
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
        {
            mage.ChangeState("MOVE");
        }

    }

    public override void Exit()
    {
        base.Exit();
        timer = 0;
    }
}
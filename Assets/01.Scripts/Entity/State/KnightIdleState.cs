using UnityEngine;

public class KnightIdleState : EntityState
{
    private Unit knight;
    
    private float idleTime;
    private float timer;

    private readonly float DEFULAT_IDLE_TIME = 2.5f;
    
    public KnightIdleState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        knight = entity as Unit;
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = DEFULAT_IDLE_TIME;
        idleTime -= entity.GetCompo<EntityStatController>().GetValue(StatType.AttackSpeed);
        
        Debug.Log(idleTime);
        
    }
    
    public override void Update()
    {
        base.Update();
        
        timer += Time.deltaTime;
        if (timer >= idleTime)
            knight.ChangeState("MOVE");
        
        /*if(Input.GetKeyDown(KeyCode.P))
            knight.ChangeState("MOVE");

        if(Input.GetKeyDown(KeyCode.A))
            knight.ChangeState("ATTACK");

        if(Input.GetKeyDown(KeyCode.R))
            renderer.Flip();*/
    }

    public override void Exit()
    {
        base.Exit();
        timer = 0;
    }
}

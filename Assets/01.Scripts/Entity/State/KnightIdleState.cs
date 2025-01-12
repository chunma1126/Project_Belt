using UnityEngine;

public class KnightIdleState : EntityState
{
    private Unit knight;

    private float idleTime;
    private float timer;
    
    public KnightIdleState(Entity entity, AnimationParamSO animationParam) : base(entity, animationParam)
    {
        knight = entity as Unit;
    }

    public override void Enter()
    {
        base.Enter();

        idleTime = Random.Range(0.2f , 1.5f);
        
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

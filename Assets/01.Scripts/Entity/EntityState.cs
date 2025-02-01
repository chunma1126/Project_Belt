public class EntityState
{
    protected Entity entity;
    protected AnimationParamSO animationParam;

    protected bool isTriggerd;

    protected EntityAnimator animator;
    protected EntityRenderer renderer;
    
    public EntityState(Entity entity, AnimationParamSO animationParam)
    {
        this.entity = entity;
        this.animationParam = animationParam;

        animator = entity.GetCompo<EntityAnimator>();
        renderer = entity.GetCompo<EntityRenderer>();
    }
    
    public virtual void Enter()
    {
        animator.SetParam(animationParam,true);
        isTriggerd = false;
        
        animator.OnAnimationEndEvent += TriggerCalled;
    }
    
    public virtual void Update()
    {
        
    }
    
    public virtual void Exit()
    {
        animator.SetParam(animationParam,false);
        
        animator.OnAnimationEndEvent -= TriggerCalled;
    }
    
    public virtual void TriggerCalled()
    {
        isTriggerd = true;
        
    }
    
}
using System.Collections.Generic;

public class Unit : Entity
{
    public List<StateSO> stateList;
    private EntityStateMachine StateMachine;
    

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine(stateList , this);
        StateMachine.Initialize("IDLE");
        
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        StateMachine.CurrentStateUpdate();
    }
    
    
    
    
}

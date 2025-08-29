using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "MoveTo [Target]", category: "Action", id: "776540ed54565626f65f4a0260f610a8")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    
    
    protected override Status OnUpdate()
    {
        Vector2 direction = (Target.Value.position - Agent.Value.position).normalized;
        float distance = Vector2.Distance(Target.Value.position , Agent.Value.position);
        
        Mover.Value.Move(direction.x);
        
        
        return Status.Running;
    }
    
    protected override void OnEnd()
    {
        
    }
    
}


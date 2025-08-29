using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllAnimationEnd", story: "AllAnimationEnd [Animator]", category: "Action", id: "3f066d0bc190aa7638a4a32d4fc3207d")]
public partial class AllAnimationEndAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Animator;

    protected override Status OnStart()
    {
        foreach (var item in Animator.Value.parameters)
        {
            Animator.Value.SetBool(item.nameHash, false);
        }
                
        return Status.Success;
    }
}


using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Change State")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Change State", message: "Change [EnemyState]", category: "Events", id: "aeaafd117b948e2bf46a354de3d65776")]
public partial class ChangeState : EventChannelBase
{
    public delegate void ChangeStateEventHandler(EnemyState EnemyState);
    public event ChangeStateEventHandler Event; 

    public void SendEventMessage(EnemyState EnemyState)
    {
        Event?.Invoke(EnemyState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<EnemyState> EnemyStateBlackboardVariable = messageData[0] as BlackboardVariable<EnemyState>;
        var EnemyState = EnemyStateBlackboardVariable != null ? EnemyStateBlackboardVariable.Value : default(EnemyState);

        Event?.Invoke(EnemyState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChangeStateEventHandler del = (EnemyState) =>
        {
            BlackboardVariable<EnemyState> var0 = vars[0] as BlackboardVariable<EnemyState>;
            if(var0 != null)
                var0.Value = EnemyState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChangeStateEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChangeStateEventHandler;
    }
}


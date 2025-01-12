using System;
using UnityEngine;

public interface IDamageable
{
        
    public void TakeDamage(ActionData _actionData);
    public void Dead();
    
}

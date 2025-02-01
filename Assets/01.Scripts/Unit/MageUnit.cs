using System;
using UnityEngine;

public class MageUnit : Unit
{

    public bool canShowAttackGizmo;
    
    protected override void ActiveSkill()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (canShowAttackGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , attackRadius);
        }
        
    }
}

using System;
using UnityEngine;

public class MageUnit : Unit
{
    public float attackRadius;
    
    public PoolableMono projectile;
    public Transform firePos;
    
    public bool canShowAttackGizmo;
        
    private void Start()
    {
        GetCompo<EntityAnimator>().OnAttackEvent += Fire;
    }

    private void OnDestroy()
    {
        GetCompo<EntityAnimator>().OnAttackEvent -= Fire;
    }
    
    private void Fire()
    {
        PoolableMono newProjectile = PoolManager.Instance.Pop(projectile.type);
        newProjectile.transform.position = firePos.position;

        Vector2 fireDirection = (Target.position - transform.position).normalized;
        
        (newProjectile as Projectile).Fire(fireDirection);
    }

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

using UnityEngine;

public class DamageCaster : MonoBehaviour,IEntityComponent
{
    [SerializeField] private LayerMask whatIsTarget;
    
    [SerializeField] private Transform damageCasterTrm;
    [SerializeField] private float radius = 1f;

    private Collider2D[] targets;

    private EntityAnimator entityAnimator;
    private EntityStatController statController;
    
    public void Initialize(Entity _entity)
    {
        entityAnimator = _entity.GetCompo<EntityAnimator>();
        statController = _entity.GetCompo<EntityStatController>();
    }
    
    private void Start()
    {
        targets = new Collider2D[5];

        entityAnimator.OnAttackEvent += Cast;
    }

    private void OnDestroy()
    {
        entityAnimator.OnAttackEvent -= Cast;
    }

    private void Cast()
    {
        RaycastHit2D[] targets = Physics2D.CircleCastAll(damageCasterTrm.position, radius, Vector2.zero, 0f, whatIsTarget);
        
        foreach (var item in targets)
        {
            if (item.collider.TryGetComponent(out IDamageable health))
            {
                ActionData actionData = new ActionData
                {
                    damageAmount = statController.GetValue(StatType.Attack),
                    knockbackPower = 1f,
                    hitPoint = item.point,
                    hitNormal = item.normal
                };
                
                health.TakeDamage(actionData);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position , radius);
    }

   
}

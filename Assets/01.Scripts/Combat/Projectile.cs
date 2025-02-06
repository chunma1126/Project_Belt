using Combat;
using UnityEngine;

public class Projectile : PoolableMono
{
    public PoolableMono explosion;

    public float fireSpeed;
    private Rigidbody2D rigidbody2D;
    
    public override void Initialize()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamageable health))
            {
                Vector2 hitPosition = other.ClosestPoint(transform.position); 
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; 

                ActionData actionData = new ActionData();
                actionData.damageAmount = 5;
                actionData.knockbackPower = 3;
                actionData.hitPoint = hitPosition;
                actionData.hitNormal = knockbackDirection;
                
                actionData.damageAmount = 5;
                health.TakeDamage(actionData);

                PoolableMono newExplosion = PoolManager.Instance.Pop(explosion.type);
                newExplosion.transform.position = hitPosition;
                
                PoolManager.Instance.Push(this);
            }
        }
    }

    public void Fire(Vector2 _direction)
    {
        rigidbody2D.linearVelocity = (_direction * fireSpeed);

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle +180);
    }

    

    public override void ResetItem()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
    }
    
    
}

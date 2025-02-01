using System;
using Combat;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
            }
        }
    }

}

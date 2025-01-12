using System;
using UnityEngine;
using UnityEngine.Events;

public class SlimeHealth : MonoBehaviour,IDamageable
{
    public UnityEvent<ActionData> HitEvent;
    public UnityEvent DeadEvent;

    public readonly float maxHealth = 100;
    public float currentHealth;

    public GameObject testParticle;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(ActionData _actionData)
    {
        currentHealth -= _actionData.damageAmount;

        if (currentHealth <= 0)
        {
            Dead();
        }
        
        HitEvent?.Invoke(_actionData);
    }

    public void Dead()
    {
        DeadEvent?.Invoke();
    }

    public void PlayParticle(ActionData _actionData)
    {
        ParticleSystem hitParticle = Instantiate(testParticle , _actionData.hitPoint , 
           Quaternion.identity).GetComponent<ParticleSystem>();

        hitParticle.Simulate(0);
        hitParticle.Play();
        
    }
}
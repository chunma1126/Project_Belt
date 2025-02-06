using System;
using UnityEngine;

public class Explosion : PoolableMono
{
    private ParticleSystem _particleSystem;

    public float time;
    private float timer = 0;
    
    public override void Initialize()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= time)
            PoolManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        timer = 0;
        
        _particleSystem.Simulate(0);
        _particleSystem.Play();
    }
}

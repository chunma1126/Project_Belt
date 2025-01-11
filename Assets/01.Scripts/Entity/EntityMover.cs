using UnityEngine;

public class EntityMover : MonoBehaviour,IEntityComponent
{
    private Rigidbody2D rigidbody2D;
    private Entity entity;
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float _speed)
    {
        float testSpeed = 5;
        rigidbody2D.linearVelocityX = _speed * testSpeed;
    }

    public void Jump(float _amount)
    {
        rigidbody2D.linearVelocityY = _amount;
    }
    
    public void StopImmediately(bool resetY = false)
    {
        
        if (resetY)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }
        else
        {
            rigidbody2D.linearVelocityX = 0;
        }
        
        
    }
    
    
}

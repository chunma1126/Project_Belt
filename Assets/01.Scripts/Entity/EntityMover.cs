using UnityEngine;

public class EntityMover : MonoBehaviour,IEntityComponent
{
    private Rigidbody2D rigidbody2D;
    
    private Entity entity;
    private EntityRenderer renderer;
    
    private EntityStatController _entityStatController;
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;

        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = entity.GetCompo<EntityRenderer>();
        _entityStatController = entity.GetCompo<EntityStatController>();
    }

    public void Move(float _direction)
    {
        float speed = _entityStatController.GetValue(StatType.MoveSpeed);
        
        rigidbody2D.linearVelocityX = _direction * speed;
        
        if(_direction < 0 && renderer.lookRight)
            renderer.Flip();
        else if(_direction > 0 && renderer.lookRight == false)
            renderer.Flip();
        
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

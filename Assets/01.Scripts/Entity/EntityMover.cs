using System;
using UnityEngine;

public class EntityMover : MonoBehaviour,IEntityComponent
{
    [Header("GroundCheck info")] 
    public LayerMask whatIsGround;
    public float checkGroundDistance;

    [Header("WallCheck info")] 
    public Transform checkWall;
    public float checkWallDistance;
    
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
        
        
        int direction = entity.GetCompo<EntityRenderer>().lookDirection;
        if (IsWall(direction) && IsGround())
        {
            Jump(5);
        }
        
        //print($"wall : {IsWall(direction)} , ground : {IsGround()}");
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

    public bool IsGround() =>  Physics2D.Raycast(transform.position , Vector2.down , checkGroundDistance,whatIsGround);
    public bool IsWall(int _direction) => Physics2D.Raycast(checkWall.position , Vector2.right * _direction ,checkWallDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position , Vector2.down * checkGroundDistance);
        Gizmos.DrawRay(checkWall.position , Vector2.right * checkWallDistance);
    }
}

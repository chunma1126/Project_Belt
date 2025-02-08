using UnityEngine;

public class EntityMover : MonoBehaviour, IEntityComponent
{
    [Header("GroundCheck info")] 
    public bool isGround;
    public LayerMask whatIsGround;
    public float checkGroundDistance;
    public Vector2 checkGroundSize;

    [Header("WallCheck info")] 
    public bool isWall;
    public Transform checkWall;
    public Vector2 checkWallSize;

    [Header("CanMove info")] 
    public bool isCanMove;
    public Transform checkCanMove;
    public Vector2 checkCanMoveSize;

    [Range(1, 20)] public float jumpForce;
    
    private Rigidbody2D rigidbody2D;
    
    private Entity entity;
    private EntityRenderer renderer;
    
    private EntityStatController _entityStatController;

    public bool canShowGizmo;
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;

        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = entity.GetCompo<EntityRenderer>();
        _entityStatController = entity.GetCompo<EntityStatController>();
    }

    private void Update()
    {
        //IsCanMove(renderer.lookDirection);
    }

    public bool Move(float _direction)
    {
        if(IsCanMove(_direction) == false)
        {
            StopImmediately();
            return false;
        }
        
        float speed = _entityStatController.GetValue(StatType.MoveSpeed);

        rigidbody2D.linearVelocityX =  _direction * speed;
        
        if (_direction < 0 && renderer.lookRight)
            renderer.Flip();
        else if (_direction > 0 && renderer.lookRight == false)
            renderer.Flip();
        
        int direction = entity.GetCompo<EntityRenderer>().lookDirection;
        if (IsGround() && IsWall(direction))
        {
            Jump(jumpForce);
        }
        
        return true;
    }

    private void Jump(float _amount)
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

    private bool IsGround()
    {
        isGround = Physics2D.BoxCast(transform.position, checkGroundSize, 0f, Vector2.down, checkGroundDistance, whatIsGround);
        return isGround;
    }
    
    private bool IsWall(int _direction)
    {
        isWall =Physics2D.BoxCast(checkWall.position, checkWallSize, 0f, Vector2.right * _direction, 0, whatIsGround);
        return isWall;
    }
        
    private bool IsCanMove(float _direction)
    {
        isCanMove =Physics2D.BoxCast(checkCanMove.position, checkCanMoveSize, 0f, Vector2.right * _direction, 0, whatIsGround);
        return isCanMove;
    }

    private void OnDrawGizmos()
    {
        if(!canShowGizmo)return;
        
        Gizmos.color = Color.white;
   
        // Ground check - 실제 BoxCast 범위와 동일하게 표시
        Gizmos.DrawWireCube(transform.position + (Vector3.down * checkGroundDistance/2), checkGroundSize);
   
        // Wall check - 실제 BoxCast 범위와 동일하게 표시 
        Gizmos.DrawWireCube(checkWall.position, checkWallSize);
        
        Gizmos.DrawWireCube(checkCanMove.position, checkCanMoveSize);
    }
}

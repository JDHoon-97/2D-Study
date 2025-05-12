using System;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected BaseKnife _knife;
    [SerializeField] protected Transform _transform;
    [SerializeField] protected float _moveSpeed = 10;
    
    private int groundMask = 1;
    protected float _xMovement;
    public bool _isJumping;
    public float Direction { get; set; }
    
    public bool IsAttacking { get; set; }
    public Animator Animator => _animator;
    
    protected virtual void Awake()
    {
        //최적화에 도움이 됨. 마샬링 -> 블로그
        _transform = transform;
    }
    
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }

    public abstract void Attacking();

    protected virtual void Update()
    {
        bool isMoving = _xMovement != 0;
        
        if (isMoving == false)
        {
            _rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;

        }
        else
        {
            _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
        
        _animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        const float distance = 1.0f;
        Vector2 perp = Vector2.zero;
        bool isSlope = false;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundMask);

        if (hit)
        {
            perp = Vector2.Perpendicular(hit.normal).normalized;
            float angle = Vector2.Angle(hit.normal, Vector2.up);

            if (angle != 0)
            {
                isSlope = true;
            }

        }
        
        if(isSlope)
            _rigidbody.linearVelocityX = perp.x * _xMovement * -1f;
        else
            _rigidbody.linearVelocityX = _xMovement;
        
        if (Mathf.Approximately(_xMovement, 0.0f) == false)
        {
            float y = _xMovement > 0.0f ? 0.0f : -180.0f;
            _transform.rotation = Quaternion.Euler(0, y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        const float distance = 1.0f;
        Vector2 perp = Vector2.zero;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundMask);

        if (hit)
        {
            perp = Vector2.Perpendicular(hit.normal).normalized;
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
            Debug.DrawLine(hit.point, hit.point + perp, Color.red);
        }
    }
}

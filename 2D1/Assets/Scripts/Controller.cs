using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Controller : BaseController
{
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _jumpPower = 10;
    [SerializeField] private float _moveSpeed = 10;
    
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletPoint;
    private float distance = 1;
    private int groundMask = 1;
    public float angle;
    public Vector2 perp;
    private void Awake()
    {
        //최적화에 도움이 됨. 마샬링 -> 블로그
        _transform = transform;
    }
    
    private void Update()
    {
        bool isMoving = false;
        bool isUp = false;
        bool isDown = false;
        bool isSlope = false;
        
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, distance, groundMask);

        if (hit)
        {
            perp = Vector2.Perpendicular(hit.normal).normalized;
            angle = Vector2.Angle(hit.normal, Vector2.up);

            if (angle != 0)
            {
                isSlope = true;
            }

            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
            Debug.DrawLine(hit.point, hit.point + perp, Color.red);

        }
        
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(isSlope)
                _rigidbody.linearVelocityX = perp.x * -_moveSpeed * -1f;
            else
            _rigidbody.linearVelocityX = -_moveSpeed;
            
            isMoving = true;
            
            //블로그 과제 : 쿼터니언
            _transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(isSlope)
                _rigidbody.linearVelocityX = perp.x * _moveSpeed * -1f;
            else
            _rigidbody.linearVelocityX = _moveSpeed;

            isMoving = true;

            _transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (isMoving == false)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        // 위 방향키를 누르면 위쪽을 본다.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isUp = true;
        }
        
        // 아래 방향키를 누르면 아래를 본다.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDown = true;
        }
        
        //버튼 한번 누르면 계속 공격이 재생
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attacking();
        }
        
        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //추친력 생성
            if (_isJumping == false)
            {
                _isJumping = true;
                _rigidbody.AddForceY(_jumpPower, ForceMode2D.Impulse);
            }
        }

        //사망
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    _upperRenderer.enabled = false;
        //    _animator.SetTrigger("Dead");
        //}
        
        //수류탄 투척
        if (Input.GetKeyDown(KeyCode.C))
        {
            _animator.SetTrigger("IsThrowBomb");
        }
        
        _animator.SetBool("IsJumping", _isJumping);
        _animator.SetBool("IsUp", isUp);
        _animator.SetBool("IsMoving", isMoving);
        _animator.SetBool("IsDown", isDown);
    }

    public override void Attacking()
    {
        if (IsAttacking == false)
        {
            IsAttacking = true;

            if (_knife.CanAttack)
                _knife.Attack();
            else
                Shoot();
        }
    }

    private void Shoot()
    {
        _animator.SetTrigger("Attack");

        Transform bullet = Instantiate(_bullet).transform;
        bullet.position = _bulletPoint.position;
        bullet.rotation = _transform.rotation;
    }
}

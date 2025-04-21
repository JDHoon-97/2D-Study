using UnityEngine;

public class Controller : BaseController
{
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private float _jumpPower = 10;
    [SerializeField] private float _moveSpeed = 10;
    
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletPoint;
    
    protected override void Update()
    {
        base.Update();

        _xMovement = 0;

        bool isUp = false;
        bool isDown = false;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _xMovement = -_moveSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _xMovement = _moveSpeed;
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

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Controller : BaseController
{
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private float _jumpPower = 10;
    [SerializeField] private float _moveSpeed = 10;
    
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletPoint;

    private List<bool> _bulletQueue = new List<bool>();
    
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
            //과제 : Up > UpAttack 전환시 부드럽게 애니메이션 수정하기 ( Up 클립 수정 )
            isUp = true;
            
            //TODO : 위치, 회전 수정
        }
        
        // 아래 방향키를 누르면 아래를 본다.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDown = true;
            
            //TODO : 위치, 회전 수정
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
        if(_bulletQueue.Count < 2 )
            _bulletQueue.Add(true);
        
        if (IsAttacking == false && _bulletQueue.Count > 0 )
        {
            _bulletQueue.RemoveAt(0);
            
            IsAttacking = true;

            if (_knife.CanAttack)
                _knife.Attack();
            else
                StartCoroutine (Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        //과제 : 코루틴 정리
        _animator.SetTrigger("Attack");

        yield return null; 
        
        Transform bullet = Instantiate(_bullet).transform;
        bullet.position = _bulletPoint.position;
        bullet.rotation = _bulletPoint.rotation;
    }

    public void SetAttackPossible()
    {
        //과제 : 애니메이션 이벤트 정리
        IsAttacking = false;
    }
}

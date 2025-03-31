using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private SpriteRenderer _lowerRenderer;

    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField] private float _jumpPower = 10;
    [SerializeField] private float _moveSpeed = 10;

    [SerializeField] private Knife _knife;
    
    private bool _isJumping = false;

    public bool IsAttacking { get; set; }
    public Animator Animator => _animator;
    private void Update()
    {
        bool isMoving = false;
        bool isUp = false;
        bool isDown = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.linearVelocityX = -_moveSpeed;
            
            isMoving = true;
            _upperRenderer.flipX = true;
            _lowerRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.linearVelocityX = _moveSpeed;

            isMoving = true;
            _upperRenderer.flipX = false;
            _lowerRenderer.flipX = false;
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
            if (IsAttacking == false)
            {
                IsAttacking = true;
                
                if (_knife.CanAttack)
                    _knife.Attack();
                else
                    _animator.SetTrigger("Attack");  
            }
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            _upperRenderer.enabled = false;
            _animator.SetTrigger("Dead");
        }
        
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}

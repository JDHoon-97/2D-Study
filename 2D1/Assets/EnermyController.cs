using UnityEngine;

public class EnermyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Knife _knife;
    
    private bool _isJumping = false;

    public bool IsEnermyAttacking { get; set; }
    public Animator Animator => _animator;
    private void Update()
    {
        bool isMoving = false;
        bool isUp = false;
        bool isDown = false;
        
        
        //버튼 한번 누르면 계속 공격이 재생
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (IsEnermyAttacking == false)
            {
                IsEnermyAttacking = true;
                
                if (_knife.CanAttack)
                    _knife.Attack();
                else
                    _animator.SetTrigger("EnermyAttack");  
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}


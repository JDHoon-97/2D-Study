using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //생성자
    //유니티의 MonoBehaviour는 생성자를 사용할 수 없다.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private SpriteRenderer _lowerRenderer;

    private Transform _cachedTransform;
    [SerializeField] private float _jumpPower = 1;
    private float _currentJumpPower;

    private bool _isJumping = false;
    
    private void Start()
    {
        _cachedTransform = transform;
    }
    private void Update()
    {
        bool isMoving = false;
        bool isUp = false;
        bool isDown = false;
        bool isDead = false;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = _cachedTransform.position;
            position.x -= 1 * Time.deltaTime;
            _cachedTransform.position = position;
            
            isMoving = true;
            _upperRenderer.flipX = true;
            _lowerRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = _cachedTransform.position;
            position.x += 1 * Time.deltaTime;
            _cachedTransform.position = position;

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
            _animator.SetTrigger("IsAttack");
            _animator.SetTrigger("IsUpAttack");
            _animator.SetTrigger("IsDownAttack");
        }
        
        //점프
        if (Input.GetKey(KeyCode.Space))
        {
            //추친력 생성
            if (_isJumping == false)
            {
                _isJumping = true;
                _currentJumpPower = _jumpPower;
            }
        }
        
        if (_isJumping == true)
        {
            Jump();
        }
        
        //사망
        if (Input.GetKey(KeyCode.R))
        {
            _upperRenderer.enabled = false;
            isDead = true;
        }
        
        //칼 공격
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetTrigger("IsKnifeAttack");
        }
        
        //수류탄 투척
        if (Input.GetKeyDown(KeyCode.C))
        {
            _animator.SetTrigger("IsThrowBomb");
        }
        _animator.SetBool("IsMoving", isMoving);
        _animator.SetBool("IsUp", isUp);
        _animator.SetBool("IsDown", isDown);
        _animator.SetBool("IsJumping", _isJumping);
        _animator.SetBool("IsDead", isDead);
    }

    private void Jump()
    {
        //y 값 이동
        Vector3 position = _cachedTransform.position;
        position.y += _currentJumpPower * Time.deltaTime;
        _cachedTransform.position = position;
        
        //추진력 감소
        _currentJumpPower -= Time.deltaTime;
        
        //땅에 닿으면 _isJumping 비활성화, y 값을 0
        if (_cachedTransform.position.y < 0)
        {
            _isJumping = false;
            position.y = 0;
            _cachedTransform.position = position;
        }
    }
}

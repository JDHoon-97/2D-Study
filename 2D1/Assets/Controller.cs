using UnityEngine;

public class Controller : MonoBehaviour
{
    //생성자
    //유니티의 MonoBehaviour는 생성자를 사용할 수 없다.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        //게임오브젝트는 기본적으로 하나의 트랜스폼을 가지고 있다.
        
        //생성자처럼 사용할 수 있다.
    }

    // Update is called once per frame
    void Update()
    {
        //사용자마다 프레임의 수행 속도가 다르다.
        // 프레임 속도가 빠른 경우 1초에 1프레임 >> 3초에 3프레임 >> 3칸 움직임
        //'' 느린 경우 3초에 1프레임 >> 1칸 움직임
        bool isMoving = false;
        bool isAttack = false;
        
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 position = transform.position;
            position.x -= 1 * Time.deltaTime;
            transform.position = position;
            
            isMoving = true;
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 position = transform.position;
            position.x += 1 * Time.deltaTime;
            transform.position = position;

            isMoving = true;
            _spriteRenderer.flipX = false;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            isAttack = true;
        }
        
        _animator.SetBool("IsMoving", isMoving);
        _animator.SetBool("IsAttack", isAttack);
    }
}

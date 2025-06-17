using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] public float _hp;
    [SerializeField] private Animator _animator;
    
    private Rigidbody2D _rigidbody2D;
    private bool _isDestroyed = false;
    private float _downmoveSpeed = 1;

    private void Start()
    {
        _isDestroyed = false;
    }
    private void Update()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.linearVelocity = new Vector2(-_moveSpeed, _rigidbody2D.linearVelocity.y);
        
        BoatDestroy();
        
        if (_isDestroyed)
        {
            transform.position += Vector3.up * _downmoveSpeed * Time.deltaTime;
        }
    }

    public void BoatDestroy()
    {
        if (_hp == 0)
        {
            _animator.SetTrigger("BoatDestroy");
            _moveSpeed = 0;
            
            Destroy(gameObject, 2f);
        }
    }

    public void Destroy()
    {
        Destroy(GetComponent<BoxCollider2D>());
        _isDestroyed = true;
    }
}

using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] public float _moveSpeed;
    [SerializeField] public float _hp;
    [SerializeField] private Animator _animator;
    
    public Rigidbody2D _rigidbody2D;
    public bool _isDestroyed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (_hp == 0)
        {
            BoatDestroy();
        }

        if(!_isDestroyed)
            _rigidbody2D.linearVelocity = new Vector2(-_moveSpeed, _rigidbody2D.linearVelocity.y);
        
    }
    
    public void BoatDestroy()
    {
        //TODO : hp가 -1이 되야지 떨어짐
        //TODO : Boat Water가 계속 재생
        
        _animator.SetTrigger("BoatDestroy");
        _moveSpeed = 0;
        //Destroy(gameObject, 5f);
    }

    public void OnDestroyed()
    {
        Transform water = transform.Find("Boat Water");
        _isDestroyed = true;
        
        Destroy(GetComponent<BoxCollider2D>());
        
        _rigidbody2D.gravityScale = 0.1f;
        
        water.parent = null;
    }
}

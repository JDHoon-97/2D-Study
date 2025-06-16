using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _lifeTime = 2;
    [SerializeField] protected Player _player;
    
    protected float _currentLifeTime;
    private void Start()
    {
        Vector3 bubbleDirection = transform.right;
        _rigidbody.AddForce(bubbleDirection * _speed, ForceMode2D.Impulse);
        _currentLifeTime = _lifeTime;
        
        _player = GameManager.Instance.Player;
    }
    
    private void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if(_currentLifeTime <= 0)
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.TakeDamage(1);
            _rigidbody.linearVelocity = Vector2.zero;
            
            Destroy(gameObject,0.2f);
        }
    }
}

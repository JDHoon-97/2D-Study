using UnityEngine;

public class EnermyWeapon : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _lifeTime = 2;
    [SerializeField] protected Player _player;
    
    protected float _currentLifeTime;

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
    }

    protected virtual void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if(_currentLifeTime <= 0)
            Destroy(gameObject);
    }

    protected virtual void OntriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage(1);
            _rigidbody.linearVelocity = Vector2.zero;
            
            Destroy(gameObject,0.2f);
        }
    }
}

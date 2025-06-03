using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 2;
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    private float _cuurrentLifeTime;
    private void Start()
    {
        Vector3 bubbleDirection = transform.right;
        _rigidbody.AddForce(bubbleDirection * _speed, ForceMode2D.Impulse);
        _cuurrentLifeTime = _lifeTime;
    }

    private void Update()
    {
        _cuurrentLifeTime -= Time.deltaTime;

        if (_cuurrentLifeTime <= 0)
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.TakeDamage(1);
            _animator.SetTrigger("IsHit");
            _rigidbody.linearVelocity = Vector2.zero;
            
            Destroy(gameObject,0.2f);
        }
    }
}

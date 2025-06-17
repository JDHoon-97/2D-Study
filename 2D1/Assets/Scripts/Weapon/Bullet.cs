using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 2;
    [SerializeField] private Animator _animator;
    private float _cuurrentLifeTime;
    private void Start()
    {
        Vector3 bulletDirection = transform.right;
        _rigidbody.AddForce(bulletDirection * _speed, ForceMode2D.Impulse);
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
        if (other.gameObject.CompareTag("Enermy")||other.gameObject.CompareTag("Crab"))
        {
            var enermy = other.gameObject.GetComponent<Enermy>();
            enermy.TakeDamage(1);
            _animator.SetTrigger("IsHit");
            _rigidbody.linearVelocity = Vector2.zero;
            
            Destroy(gameObject,0.2f);
        }

        if (other.gameObject.CompareTag("Boat"))
        {
            var boat = other.gameObject.GetComponent<Boat>();
            _animator.SetTrigger("IsHit");
            _rigidbody.linearVelocity = Vector2.zero;
            boat._hp -= _damage;
            
            Destroy(gameObject,0.2f);
        }
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 2;

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

    //TODO Use bullet pull
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enermy"))
        {
            var enermy = other.gameObject.GetComponent<Enermy>();
            enermy.TakeDamage(1);
            
            Destroy(gameObject);
        }
    }
}

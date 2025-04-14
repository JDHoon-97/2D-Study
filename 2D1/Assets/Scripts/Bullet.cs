using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private void Start()
    {
        Vector3 bulletDirection = transform.right;
        _rigidbody.AddForce(bulletDirection * _speed, ForceMode2D.Impulse);
        
    }
}

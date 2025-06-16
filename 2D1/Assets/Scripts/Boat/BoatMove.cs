using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] public float _hp;
    [SerializeField] private Animator _animator;
    
    private Rigidbody2D _rigidbody2D;

    private void Update()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.linearVelocity = new Vector2(-_moveSpeed, _rigidbody2D.linearVelocity.y);
        
        BoatDestroy();
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
    
    
}

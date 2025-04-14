using System;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected BaseKnife _knife;
    
    public bool _isJumping = false;

    public bool IsAttacking { get; set; }
    public Animator Animator => _animator;

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }

    public abstract void Attacking();
}

using System;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] public Animator _animator;
    [SerializeField] public Rigidbody2D _rigidbody;

    public bool _isJumping = false;

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

using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Character : MonoBehaviour
{
    [SerializeField] public int _hp;
    [SerializeField] public Animator _animator;
    [SerializeField] private SpriteRenderer _upperRenderer;
    [SerializeField] private BaseController _controller;
    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        if (_hp == 0)
        {
            var collider = _controller.GetComponent<Collider2D>();
            var rigid = _controller.GetComponent<Rigidbody2D>();
            
            _upperRenderer.enabled = false;
            _animator.SetTrigger("Dead");
            
            collider.enabled = false;
            rigid.linearVelocity = Vector2.zero;
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            _controller.enabled = false;
        }
        //Destroy(gameObject);
    }
}

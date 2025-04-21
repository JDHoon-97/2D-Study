using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Character : MonoBehaviour
{
    [SerializeField] public int _hp;
    [SerializeField] protected Animator _animator;
    [SerializeField] private SpriteRenderer _upperRenderer;

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
            _upperRenderer.enabled = false;
            _animator.SetTrigger("Dead");
        }
        //Destroy(gameObject);
    }
}

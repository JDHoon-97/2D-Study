using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Character : MonoBehaviour
{
    [SerializeField] private int _hp;

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
        Destroy(gameObject);
    }
}

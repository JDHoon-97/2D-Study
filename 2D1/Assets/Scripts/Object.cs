using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] public int _hp;
    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}

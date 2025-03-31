using UnityEngine;

public class Enermy : MonoBehaviour
{
    [SerializeField] private int _hp = 3;
    
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        
        if(_hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}

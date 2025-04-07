using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 10;
    
    public void TakePlayerDamage(int damage)
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
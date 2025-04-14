using UnityEngine;

public class Player : Character
{
    [SerializeField] private int _playerhp = 10;
    
    public void TakePlayerDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
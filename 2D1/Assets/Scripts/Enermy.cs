using UnityEngine;

public class Enermy : Character
{
    [SerializeField] private int _enermyhp = 3;
    
    public void TakeEnermyDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}

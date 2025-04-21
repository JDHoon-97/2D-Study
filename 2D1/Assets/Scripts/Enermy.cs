using UnityEngine;

public class Enermy : Character
{
    public void TakeEnermyDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    
    public override void Dead()
    {
        if (_hp < 0)
        {
            _animator.SetTrigger("EnermyDead");
        }
        //Destroy(gameObject);
    }
}

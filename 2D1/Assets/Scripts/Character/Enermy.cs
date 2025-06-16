using UnityEngine;

public class Enermy : Character
{
    public bool IsDead => _hp <= 0;
    
    public override void Dead()
    {
        if (_hp == 0)
        {
            _animator.SetTrigger("EnermyDead");
        }
        
        Destroy(gameObject, 3f);
    }
}

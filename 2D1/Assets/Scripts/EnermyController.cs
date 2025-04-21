using UnityEngine;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    
    protected override void Update()
    {
        base.Update();
        
        //버튼 한번 누르면 계속 공격이 재생
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attacking();
        }
    }

    public override void Attacking()
    {
        if (!IsAttacking)
        {
            IsAttacking = true;

            if (_knife.CanAttack)
                _knife.Attack();
        }
    }
    
    
}


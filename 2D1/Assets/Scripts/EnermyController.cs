using UnityEngine;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    
    private void Update()
    {
        bool enermyIsMoving = false;
        
        //버튼 한번 누르면 계속 공격이 재생
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attacking();
        }
        
        if (enermyIsMoving == false)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
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


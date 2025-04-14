using UnityEngine;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private EnermyKnife _enermyKnife;

    public bool IsEnermyAttacking { get; set; }
    private void Update()
    {
        //버튼 한번 누르면 계속 공격이 재생
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attacking();
        }
    }

    public override void Attacking()
    {
        if (!IsEnermyAttacking)
        {
            IsEnermyAttacking = true;

            if (_enermyKnife.CanAttack)
                _enermyKnife.Attack();
            else
                _animator.SetTrigger("EnermyAttack");
        }
    }
}


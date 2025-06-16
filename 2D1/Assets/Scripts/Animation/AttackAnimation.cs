using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{   //SpecialAttacking 애니메이션
    private BaseController _controller;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            _controller = animator.GetComponent<BaseController>();
        }

        if (_controller != null)
        {
            _controller.IsAttacking = false;
        }
    }
}

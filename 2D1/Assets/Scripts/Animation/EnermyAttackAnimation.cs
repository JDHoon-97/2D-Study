using UnityEngine;

public class EnermyAttackAnimation : StateMachineBehaviour
{   
    private EnermyController _controller;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            _controller = animator.GetComponent<EnermyController>();
        }

        if (_controller != null)
        {
            _controller.IsSpecialAttacking = false;
        }
    }
}

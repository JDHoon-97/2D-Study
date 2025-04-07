using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    private Controller _controller;
    private EnermyController _enermyController;
    
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            _controller = animator.GetComponent<Controller>();
        }

        if (_enermyController == null)
        {
            _enermyController = animator.GetComponent<EnermyController>();
        }
        
        _controller.IsAttacking = false;
        _enermyController.IsEnermyAttacking = false;
    }
}

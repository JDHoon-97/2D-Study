using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    private Controller _controller;
    
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            _controller = animator.GetComponent<Controller>();
        }
        
        _controller.IsAttacking = false;
    }
}

using UnityEngine;

public class Behavior_Pyramid_Main : StateMachineBehaviour
{
    public Enemy enemy;

    private Transform target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get player object
        target = GameObject.FindWithTag("Player").transform;
        //Set collider based on enemy sprite
        animator.gameObject.AddComponent<PolygonCollider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject != null)
        {
            //Rotate based on player location
            animator.transform.right = animator.transform.position - target.position;
            animator.transform.position += -animator.transform.right * enemy.speed * Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Sphere_Main : StateMachineBehaviour
{
    public Enemy enemy;

    public int range;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set collider based on enemy sprite
        animator.gameObject.AddComponent<PolygonCollider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int c = 0;
        Collider2D[] inRange = Physics2D.OverlapCircleAll(animator.transform.position, range);
        foreach (Collider2D col in inRange)
        {
            if (col.tag == "Player")
            {
                c++;
                break;
            }
        }

        if (c > 0)
        {
            animator.SetTrigger("Charge");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Charge");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Cube_Main : StateMachineBehaviour
{
    public Enemy enemy;

    private Transform target;

    public float range;
    public Shoot shoot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get player object
        target = GameObject.FindWithTag("Player").transform;
        //Set collider based on enemy sprite
        animator.gameObject.AddComponent<PolygonCollider2D>();
        shoot = animator.GetComponent<Shoot>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject != null)
        {
            //Rotate based on player location
            animator.transform.right = animator.transform.position - target.position;

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
                shoot.enabled = true;
            }
            else
            {
                shoot.enabled = false;
                animator.transform.position += -animator.transform.right * enemy.speed * Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Shoot");
    }
}

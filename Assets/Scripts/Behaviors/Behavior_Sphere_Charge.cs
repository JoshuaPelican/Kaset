using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Sphere_Charge : StateMachineBehaviour
{
    public Enemy enemy;

    public AudioClip chargeSound;
    private Vector3 chargeLocation;

    public float chargeSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get player object
        chargeLocation = GameObject.FindWithTag("Player").transform.position;
        //Set collider based on enemy sprite
        animator.gameObject.AddComponent<PolygonCollider2D>();
        animator.GetComponent<AudioSource>().clip = chargeSound;
        animator.GetComponent<AudioSource>().Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Rotate based on player location
        animator.transform.right = animator.transform.position - chargeLocation;
        animator.transform.position += -animator.transform.right * enemy.speed * chargeSpeed * Time.deltaTime;
        if(Vector3.Distance(animator.transform.position, chargeLocation) <= 1f)
        {
            animator.SetTrigger("Explode");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Explode");
    }
}

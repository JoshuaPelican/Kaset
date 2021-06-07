using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Sphere_Explode : StateMachineBehaviour
{
    public ParticleSystem explosionSystem;
    public GameObject deathSource;
    public AudioClip explosionClip;
    public float explosionRadius;
    public float explosionDamage;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject newDeathSource = Instantiate(deathSource, animator.transform.position, Quaternion.identity);
        newDeathSource.GetComponent<AudioSource>().clip = explosionClip;
        newDeathSource.GetComponent<AudioSource>().Play();

        Instantiate(explosionSystem, animator.transform.position, Quaternion.identity);

        Collider2D[] inRadius = Physics2D.OverlapCircleAll(animator.transform.position, explosionRadius);
        foreach (Collider2D col in inRadius)
        {
            if (col.tag == "Player")
            {
                col.GetComponent<Health>().TakeDamage(explosionDamage);
                break;
            }
        }

        Destroy(animator.gameObject);
    }
}

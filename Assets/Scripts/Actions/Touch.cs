using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<RewindTape>().rewinding)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}

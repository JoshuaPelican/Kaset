using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Weapon weapon;
    public AudioSource source;
    public Animation anim;

    private PolygonCollider2D col;
    private int pierce;

    private Vector2 spawnPos;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.projectile;
        col = gameObject.AddComponent<PolygonCollider2D>();
        col.isTrigger = true;
        source.clip = weapon.effectClip;
        source.Play();
        pierce = weapon.pierce;
        transform.localScale *= weapon.sizeMulti;
        spawnPos = transform.position;
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * weapon.speed;
        if(Vector2.Distance(spawnPos, transform.position) > weapon.range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weapon.playerWeapon)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(weapon.damage);
                Instantiate(weapon.projectileParticles, transform.position, Quaternion.identity);
                if(pierce > 1)
                {
                    pierce--;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (!weapon.playerWeapon)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(weapon.damage);
                Instantiate(weapon.projectileParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Weapon weapon;

    private int cooldown;
    public PlayerInput pInput;
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private void OnEnable()
    {
        if (weapon.playerWeapon)
        {
            pInput.onShoot.AddListener(ShootWeapon);
        }
    }

    private void FixedUpdate()
    {
        cooldown++;
        if (!weapon.playerWeapon)
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        if(cooldown >= weapon.rate * 60)
        {
            float nextAngle = -weapon.spread;
            for (int i = 0; i < weapon.projectileCount; i++)
            {           
                //Fire weapon
                GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.Euler(0, 0 , (shootPoint.rotation.eulerAngles.z + nextAngle)));
                //Give projectile the weapon it came from
                newProjectile.GetComponent<Projectile>().weapon = weapon;
                if(weapon.spread != 0)
                {
                    nextAngle += 2 * weapon.spread / (weapon.projectileCount - 1);
                }
            }

            //Restart cooldown
            cooldown = 0;
        }
    }


}

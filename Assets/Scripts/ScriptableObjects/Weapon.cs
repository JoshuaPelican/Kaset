using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;

    public Sprite projectile;
    public GameObject projectileParticles;
    public AudioClip effectClip;

    public float damage;
    public float rate;
    public float speed;
    public int projectileCount;
    public int spread;
    public int pierce;
    public float range;
    public float sizeMulti;
    public bool playerWeapon;
}

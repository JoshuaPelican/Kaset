using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Custom/Enemy", order = 2)]
public class Enemy : ScriptableObject
{
    public new string name;
    public GameObject deathSystem;
    public AudioClip deathClip;
    public AudioClip shootClip;
    public Weapon weapon;

    public float health;
    public float speed;
    public int scoreValue;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Custom/Level", order = 3)]
public class Level : ScriptableObject
{
    public string cassetteName;

    public AudioClip levelMusic;

    public int audioMulti;

    public int audioThresholdSmall;
    public EnemyType smallEnemy;
    public int audioThresholdMedium;
    public EnemyType mediumEnemy;
    public int audioThresholdLarge;
    public EnemyType largeEnemy;

    public int difficulty;

    public Weapon weapon;
    public Sprite weaponIcon;

    public Sprite skin;

    public enum EnemyType { Pyramid, Cube, Sphere, Bipyramid}
}

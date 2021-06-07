using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer rend;
    public AudioSource source;
    public AudioClip damagedClip;
    public GameObject deathSource;
    public GameObject healthSun;
    public float currentHealth;

    public GameObject deathSpawn;

    private bool canBeDamaged = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        if(enemy != null)
        {
            currentHealth = enemy.health;
        }
        else
        {
            currentHealth = 100;
        }
    }

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            source.clip = damagedClip;
            source.Play();

            StartCoroutine(DamageFlash());
            currentHealth -= damage;

            if (enemy == null)
            {
                canBeDamaged = false;

                healthSun.GetComponent<Animator>().ResetTrigger("Reset");
                healthSun.GetComponent<Animator>().SetTrigger("Burst");

                //Sun Health Stuff
                StopCoroutine("SunMove");
                StartCoroutine("SunMove", damage);

            }

            if (currentHealth <= 0)
            {
                if (enemy != null)
                {
                    GameObject newDeathSource = Instantiate(deathSource, transform.position, Quaternion.identity);
                    AudioSource _source = newDeathSource.GetComponent<AudioSource>();
                    _source.clip = enemy.deathClip;
                    _source.Play();

                    ScoreManager.instance.AddScore(enemy.scoreValue);

                    Instantiate(enemy.deathSystem, transform.position, Quaternion.identity);

                    if(deathSpawn != null)
                    {
                        if(enemy.name == "Bipyramid")
                        {
                            Instantiate(deathSpawn, transform.position + Vector3.up * 5f, Quaternion.identity);
                            Instantiate(deathSpawn, transform.position + Vector3.up * -5f, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    EndGameManager.instance.EndGame(false);
                }
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator SunMove(int damage)
    {
        for (int i = 1; i < 11; i++)
        {
            healthSun.transform.position = new Vector3(healthSun.transform.position.x , 7 - ((1 - (currentHealth + (damage * (1 - (i * .1f))))/100)) * 25);
            yield return new WaitForSeconds(.1f);
        }

        healthSun.GetComponent<Animator>().ResetTrigger("Burst");
        healthSun.GetComponent<Animator>().SetTrigger("Reset");
    }

    public IEnumerator DamageFlash()
    {
        if (enemy != null)
        {
            int flashDuration = 5;
            for (int i = 0; i < flashDuration; i++)
            {
                rend.color = Color.black;
                yield return new WaitForSeconds(.05f);
                rend.color = Color.white;
                yield return new WaitForSeconds(.05f);
            }
        }
        else
        {
            int flashDuration = 3;
            for (int i = 0; i < flashDuration; i++)
            {
                rend.color = new Color(1, .3f, .3f);
                yield return new WaitForSeconds(.15f);
                rend.color = Color.white;
                yield return new WaitForSeconds(.4f);
            }

            canBeDamaged = true;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class RewindTape : MonoBehaviour
{
    public int tolerance;
    public float maxTapeLength;

    public PlayerInput pInput;
    public LineRenderer rend;
    public PlayerMovement move;
    public AudioSource rewindSource;
    public Animator anim;

    public int clickCount;
    public AudioSource click;

    public float cooldownLength;
    private bool onCooldown;

    public GameObject levelManager;

    public bool rewinding;
    private Vector3[] positions;
    private int count;

    private int c = 0;
    private Vector3 oldPos;

    private void OnEnable()
    {
        pInput.rewinding.AddListener(IsRewinding);
    }

    private void FixedUpdate()
    {
        //Start counter to next record
        c++;
        if(c > tolerance)
        {
            //Can record
            if (!rewinding)
            {
                Record();
            }

            c = 0;
        }
        //Can rewind
        if (rewinding)
        {
            Rewind();
        }

        rend.endColor = new Color((1 - (rend.positionCount / maxTapeLength)) * .8f, (1 - (rend.positionCount / maxTapeLength)) * .4f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rewinding)
        {
            if(collision.gameObject.tag == "Enemy")
            {
                Health enemy = collision.gameObject.GetComponent<Health>();
                enemy.TakeDamage(enemy.enemy.health + 1);
            }
        }
    }

    public void Rewind()
    {
        //If there are positions to rewind to
        if(count > -1)
        {
            //Move to the previous position
            transform.position = positions[count];
            //Move to the next one
            count--;
            rend.positionCount--;
        }
        //No more tape
        else
        {
            //Stop rewinding
            IsRewinding(false);
        }
    }

    public void Record()
    {
        //If at tape end
        if(rend.positionCount >= maxTapeLength)
        {
            //Stretch tape
            rend.SetPosition(rend.positionCount - 1, GetComponentInParent<Transform>().position);
            //Restrict movement 
            move.LockMovement(true);

            if (!click.isPlaying && clickCount < 3)
            {
                clickCount++;
                click.Play();
            }
        }
        else
        {
            clickCount = 0;
            //Free up movement if locked
            move.LockMovement(false);

            //If not in the same spot as previous recorded position
            if(oldPos != transform.position)
            {
                //Add count to line renderer
                rend.positionCount += 1;
                //Add position to line renderer
                rend.SetPosition(rend.positionCount - 1, transform.position);
                //Store old position for movement check
                oldPos = transform.position;
            }
        }

    }

    public void IsRewinding(bool isRewinding)
    {
        //If rewind button is being pressed
        if (isRewinding == true && !onCooldown)
        {
            levelManager.GetComponent<AudioSource>().Pause();
            //Start audio
            rewindSource.Play();
            //Start rewind animation
            anim.SetBool("Rewinding", true);

            //Get all positions of the tape trail and store them
            positions = new Vector3[rend.positionCount];
            rend.GetPositions(positions);
            //Get the index of most recent position
            count = positions.Length - 1;

            //Start rewinding
            rewinding = true;

            //Start cooldown
            StartCoroutine("Cooldown");
        }
        //If rewind button is released
        else
        {
            levelManager.GetComponent<AudioSource>().UnPause();
            //Stop audio
            rewindSource.Stop();
            //Stop rewind animation
            anim.SetBool("Rewinding", false);

            //Stop rewinding
            rewinding = false;
        }       
    }

    public IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownLength);
        onCooldown = false;
    }
}

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput pInput;
    public int moveSpeed;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    private bool locked = true;

    private void OnEnable()
    {
        pInput.onMove.AddListener(MovePlayer);
        pInput.onMouse.AddListener(RotatePlayer);

        maxBounds = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.scaledPixelWidth, Camera.main.pixelHeight));
        minBounds = maxBounds * -1;

    }

    public void MovePlayer(Vector3 dir)
    {
        if (!locked)
        {
            if((transform.position + (dir * Time.deltaTime * moveSpeed)).x > maxBounds.x || (transform.position + (dir * Time.deltaTime * moveSpeed)).y > maxBounds.y || (transform.position + (dir * Time.deltaTime * moveSpeed)).x < minBounds.x || (transform.position + (dir * Time.deltaTime * moveSpeed)).y < minBounds.y)
            {

            }
            else
            {
                //Move player based on direction and movement speed
                transform.position += dir * Time.deltaTime * moveSpeed;
            }
        }
    }

    public void RotatePlayer(Vector3 mousePos)
    {
        //Rotate opposite based on mouse location
        transform.right = mousePos - transform.position;
    }

    public void LockMovement(bool canMove)
    {
        locked = canMove;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class BoolEvent : UnityEvent<bool> { }

public class Vector3Event : UnityEvent<Vector3> { }

public class PlayerInput : MonoBehaviour
{
    public Vector3Event onMove = new Vector3Event();
    public Vector3Event onMouse = new Vector3Event();
    public UnityEvent onShoot = new UnityEvent();
    public BoolEvent rewinding = new BoolEvent();

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            onShoot.Invoke();
            //Play shoot animation (if any)
        }

        //Get mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        onMouse.Invoke(mousePos);

        Vector3 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDir != Vector3.zero)
        {
            onMove.Invoke(moveDir);
            //Start move animation
            anim.SetBool("Moving", true);
        }
        else
        {
            //Stop move animation
            anim.SetBool("Moving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rewinding.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rewinding.Invoke(false);
        }
    }
}

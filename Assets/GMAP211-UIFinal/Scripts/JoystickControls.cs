using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControls : MonoBehaviour
{
    public MainMenuController mainCont;
    public Animator anim;
    private float mousePosXInit;
    private float mousePosXFin;

    private void OnMouseDown()
    {
        mousePosXInit = GetMousePos().x;
    }
    private void OnMouseDrag()
    {
        //Determine mouse drag direction 
        mousePosXFin = GetMousePos().x;
        float dir = mousePosXFin - mousePosXInit;

        anim.SetFloat("Direction", dir);

        if (dir >= .5f || dir <= -.5f)
        {

            mainCont.Joystick(dir);
        }
    }

    private void OnMouseUp()
    {
        anim.SetFloat("Direction", 0);
    }

    public Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

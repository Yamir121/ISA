using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    //The mousePosition is a vector three, where x,y,z is 0,0,0 at left bottom and 1,1,0 at right top.
    static Vector3 mousePosition;
    Transform t;

    private void Start()
    {
        t = gameObject.GetComponent<Transform>();
    }


    private void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePosition.x >= 0.998f)
        {
            Move(0);
        }

        if (mousePosition.x <= 0.002f)
        {
            Move(1);
        }

    }

    public void Move(int i)
    {
        if (i == 0)
        {
            t.position = new Vector3(t.position.x + 0.04f, t.position.y, 0);
        }
        else if (i == 1)
        {
            t.position = new Vector3(t.position.x - 0.04f, t.position.y, 0);
        }
    }




}
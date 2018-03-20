using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    private Transform t;
    //standards
    [SerializeField]
    private Vector3 standardPosition = Vector3.zero;
    [SerializeField]
    private Vector3 standardRotation = new Vector3(10, 30, 0);
    [SerializeField]
    private float standardSize = 0.65f;
    [SerializeField]
    private float zoomedSize = 0.22f;
    [SerializeField]
    private Vector3 zoomedRotation = new Vector3(0,20,0);
    [SerializeField]
    private float standardMoveSpeed = 0.04f;
    [SerializeField]
    private float zoomedMoveSpeed = 0.02f;
    //instanced vars
    private float moveSpeed;
    [SerializeField]
    private Vector2 xLimits;
    [SerializeField]
    private Vector2 yLimits;
    //for the lerp
    private bool zooming = false;
    private float f = 0.0f;
    private Transform _t;

    private void Start()
    {
        t = gameObject.GetComponent<Transform>();
        moveSpeed = standardMoveSpeed;
        xLimits = new Vector2(-2,2);
        yLimits = new Vector2(-2, 2);
    }

    public void Move(int i)
    {
        if (i == 0)
        {
            t.position = new Vector3(t.position.x + moveSpeed, t.position.y, 0);
        }
        else if (i == 1)
        {
            t.position = new Vector3(t.position.x - moveSpeed, t.position.y, 0);
        }
    }

    private void Update()
    {
        if (zooming)
        {          
            if (f < 0.98f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(standardSize, zoomedSize, f);
                Camera.main.transform.localEulerAngles = Vector3.Lerp(standardRotation, zoomedRotation, f);
                Camera.main.transform.position = Vector3.Lerp(t.position, _t.position, f);
                Debug.Log(zooming);
                f += 0.6f * Time.deltaTime;
            }
            else
            {
                zooming = false;
            }
        }
        
    }

    public void ZoomTo(Transform target)
    {
        _t = target;
        GameManager.Instance.currentState = GameManager.GameState.Zoomed;
        f = 0;
        zooming = true;
    }

    public void ZoomOut()
    {
        GameManager.Instance.currentState = GameManager.GameState.OnView;
        Camera.main.orthographicSize = standardSize;
        t.transform.localEulerAngles = standardRotation;
        t.transform.position = standardPosition;
    }

    public void Limit(Vector2 x)
    {
        Debug.Log(x.x);
        Debug.Log(x.y);
    }

    public void Limit(Vector2 x, Vector2 y)
    {
        
    }

}
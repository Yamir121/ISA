using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    public GameObject limiter;

    private Transform t;
    //standards
    [SerializeField]
    private Vector3 standardPosition = Vector3.zero;
    [SerializeField]
    private Vector3 standardRotation = new Vector3(7, 20, 0);
    [SerializeField]
    private float standardSize = 3.5f;
    [SerializeField]
    private float zoomedSize = 1f;
    [SerializeField]
    private Vector3 zoomedRotation = new Vector3(0,20,0);
    [SerializeField]
    private float standardMoveSpeed = 0.2f;
    [SerializeField]
    private float zoomedMoveSpeed = 0.05f;
    [SerializeField]
    private Vector2 standardXlimits = new Vector2(-8.14f, 38.9f);
    [SerializeField]
    private Vector2 standardYlimits;

    //instanced vars
    private float moveSpeed;
    private Vector2 xLimits;
    private Vector2 yLimits;
    //for the lerp
    private bool zooming = false;
    private float interp = 0.0f;
    private Vector3 targetPos;

    private void Start()
    {
        t = gameObject.GetComponent<Transform>();
        Reset(true);
    }

    public void MoveLeft(float acceleration)
    {
        if (t.position.x < (xLimits.y))
        {
            Debug.Log("left");
            t.position = new Vector3(t.position.x + (moveSpeed * acceleration), t.position.y, t.position.z);
        }
    }

    public void MoveRight(float acceleration)
    {
        if (t.position.x > (xLimits.x))
        {
            Debug.Log("righty");
            t.position = new Vector3(t.position.x - (moveSpeed * acceleration), t.position.y, t.position.z);
        }
    }

    public void MoveUp(float speedIncrease)
    {
        //Go up
    }

    public void MoveDown(float speedIncrease)
    {
        //Go down
    }

    private void Update()
    {
        if (zooming)
        {          
            if (interp < 0.98f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(standardSize, zoomedSize, interp);
                Camera.main.transform.localEulerAngles = Vector3.Lerp(standardRotation, zoomedRotation, interp);
                Camera.main.transform.position = Vector3.Lerp(t.position, targetPos, interp);
                interp += 0.6f * Time.deltaTime;
            }
            else
            {
                zooming = false;
            }
        }       
    }

    public void ZoomTo(Transform target)
    {
        GameManager.Instance.currentState = GameManager.GameState.Zoomed;
        targetPos = target.position;
        moveSpeed = zoomedMoveSpeed;
        interp = 0;
        zooming = true;
    }

    public void ZoomOut()
    {
        if (!zooming)
        {
            GameManager.Instance.currentState = GameManager.GameState.OnView;
            Reset();
        }
    }

    public void SetLimit(Vector3 limits)
    {
        Debug.Log(limits);
        //Instantiate(limiter, new Vector3(limits.x, 0, 0), Quaternion.identity);
        //Instantiate(limiter, new Vector3(limits.y, 0, 0), Quaternion.identity);
        xLimits = new Vector2(limits.x, limits.y);
    }

    public void SetLimit(Vector3 limits,bool limitY)
    {
        SetLimit(limits);
        yLimits = new Vector2(0,limits.z);
    }

    public void Reset()
    {
        moveSpeed = standardMoveSpeed;
        xLimits = standardXlimits;
        yLimits = standardYlimits;
        Camera.main.orthographicSize = standardSize;
        t.transform.localEulerAngles = standardRotation;
    }

    public void Reset(bool resetPosition)
    {
        Reset();
        t.position = standardPosition;
    }


}
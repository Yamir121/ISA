using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    private Transform t;
    [SerializeField]
    private Vector2 xLimit;
    [SerializeField]
    private Vector2 yLimit;
    [SerializeField]
    private float zoomedSize = 0.22f;
    [SerializeField]
    private const float standardSize = 0.65f;

    private void Start()
    {
        t = gameObject.GetComponent<Transform>();
        xLimit = new Vector2(-2,2);
        yLimit = new Vector2(-2, 2);
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

    public void ZoomTo(Transform _t)
    {
        Camera.main.orthographicSize = zoomedSize;
        t.position = _t.position;
        t.rotation = _t.rotation;
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
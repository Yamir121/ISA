using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public List<BoxCollider> zoomables;

    private Movement cameraMovement;
    private Vector3 mousePosition;

    public static InteractionManager Instance { get { return _instance; } }
    private static InteractionManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        cameraMovement = Camera.main.GetComponent<Movement>();

        foreach (Transform child in transform)
        {
            zoomables.Add(child.GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }

        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePosition.x >= 0.998f)
        {
            cameraMovement.Move(0);
        }

        if (mousePosition.x <= 0.002f)
        {
            cameraMovement.Move(1);
        }

    }

    private void Click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && GameManager.Instance.currentState == GameManager.GameState.OnView)
            {
                Debug.Log("hit");
                var c = hit.collider as BoxCollider;

                cameraMovement.ZoomTo(hit.transform);
                cameraMovement.Limit(CalcLimits(c, 0));
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Zoomed)
            {
                cameraMovement.ZoomOut();
            }
        }
    }

    private static Vector2 CalcLimits(BoxCollider col, int type)
    {
        Vector3 pos = col.transform.position;
        Vector3 f = col.transform.forward;
        Vector3 r = col.transform.right;
        Vector3 u = col.transform.up;
        Vector3 min = col.transform.TransformPoint(col.center - col.size * 0.5f) - pos;
        Vector3 max = col.transform.TransformPoint(col.center + col.size * 0.5f) - pos;
        if (type == 0) { return new Vector2(min.x, max.x); }
        if (type == 1) { return new Vector2(min.y, max.y); }
        else { return Vector2.zero; }
    }
}

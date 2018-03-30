using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [HideInInspector]
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
            cameraMovement.MoveLeft(mousePosition.x * 1.2f);
        }

        if (mousePosition.x <= 0.002f)
        {
            cameraMovement.MoveRight((1+mousePosition.x*-1) * 1.2f);
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
                try
                {
                    Debug.Log("hit");
                    //hit collider must be a boxcollider
                    BoxCollider boxCol = hit.collider as BoxCollider;
                    cameraMovement.ZoomTo(hit.transform);
                    cameraMovement.SetLimit(CalcLimits(boxCol));
                }
                catch { throw new System.Exception("Collider of Raycast.Hit must be a boxcollider"); }
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Zoomed)
            {
                cameraMovement.ZoomOut();
            }
        }
    }

    private static Vector3 CalcLimits(BoxCollider col)
    {
        Vector3 pos = col.transform.position;
        //+ or - 2 is to normalize between the camera and world space
        float xMin = (pos.x - (col.size.x * 0.5f)+2);
        float xMax = (pos.x + (col.size.x * 0.5f)-2);
        float yMax = (pos.y + (col.size.y * 0.5f));

        return new Vector3(xMin, xMax, yMax);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public List<BoxCollider> zoomables;

    private static InteractionManager _instance;
    public static InteractionManager Instance { get { return _instance; } }

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
        foreach (Transform child in transform)
        {
            zoomables.Add(child.GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Click();
        }
    }

    private void Click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.)
            {
                Debug.Log("hit");
                hit.transform.gameObject;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrailGenerator : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float trailMinDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     mousePosition.z = -Camera.main.transform.position.z;
        //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //     if (Vector3.Distance(worldPosition, transform.position) >= trailMinDistance)
        //     {
        //         trailRenderer.emitting = true;
        //         transform.position = worldPosition;
        //     }
        // }
        // else
        // {
        //     trailRenderer.emitting = false;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            trailRenderer.emitting = true;
            trailRenderer.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            trailRenderer.emitting = false;
            trailRenderer.Clear();
        }

        if (trailRenderer.emitting)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Vector3.Distance(worldPosition, transform.position) >= trailMinDistance)
            {
                transform.position = worldPosition;
            }
        }
    }
}

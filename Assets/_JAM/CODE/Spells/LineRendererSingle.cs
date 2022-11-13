using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSingle : MonoBehaviour
{
    [SerializeField] float lineDisplayTime = 0.5f; 
    LineRenderer lineRenderer;

    float currentTime = 0;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetupLineRenderer(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        currentTime = lineDisplayTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            DisableLine();
        }
    }

    /// <summary>
    /// Used when updating line while active
    /// </summary>
    void UpdateLineRenderPositionRealtime(Vector3 newStartPoint, Vector3 newEndPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, newStartPoint);
        lineRenderer.SetPosition(1, newEndPoint);
    }

    /// <summary>
    /// Gets called when the Linked Unit gets disabled
    /// </summary>
    public void DisableLine()
    {
        LineRendererRequester.instance.lineQueue.Enqueue(this);
        gameObject.SetActive(false);
    }
}

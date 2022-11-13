using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererRequester : MonoBehaviour
{
    [Header("DATA")]
    public Queue<LineRendererSingle> lineQueue = new Queue<LineRendererSingle>();

    [Header("REFERENCES")]
    public LineRendererSingle lineRendererSingle;

    // Static Reference to the Handler
    public static LineRendererRequester instance;

    public void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// We request a Healthbar for our Unit
    /// </summary>
    /// <param name="_unit"></param>
    public void RequestLine(Vector3 startPoint, Vector3 endPoint)
    {
        if (lineQueue.Count <= 0)
            CreateNewHealthbar();

        // We get the Healthbar and Set everything up
        LineRendererSingle tempLine = lineQueue.Dequeue();
        tempLine.SetupLineRenderer(startPoint, endPoint);
        tempLine.gameObject.SetActive(true);
    }

    /// <summary>
    /// We create and enqueue a New Health Bar
    /// </summary>
    void CreateNewHealthbar()
    {
        lineQueue.Enqueue(Instantiate(lineRendererSingle, this.transform));
    }
}

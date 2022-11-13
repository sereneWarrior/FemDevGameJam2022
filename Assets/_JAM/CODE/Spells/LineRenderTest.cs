using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineRendererRequester.instance.RequestLine(transform.position, new Vector3(2.39413643f, 1f, -10.6747866f));
    }
}

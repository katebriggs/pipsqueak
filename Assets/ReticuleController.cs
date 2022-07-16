using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticuleController : MonoBehaviour
{

    public LineRenderer lr;

    public void DrawReticule(Vector3 position, Vector3 inDirection)
    {

        if (Physics.Raycast(position, inDirection, out var hit))
        {
            position = hit.point;
            var normal = hit.normal; normal.y = 0;

            var fromDirection = -inDirection;
            var toDirection = Vector3.Reflect(inDirection, normal);
            lr.enabled = true;
            lr.SetPositions(new Vector3[] { position + fromDirection, position, position + toDirection });
            transform.position = position;
        }
        else
        {
            lr.enabled = false;
        }
    }
}

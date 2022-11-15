using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloater : MonoBehaviour
{
    public float floatHeight = 0.5f;

    private int count = 0;
    private int steps = 25;
    private bool up = true;

    private void FixedUpdate()
    {
        Vector3 move = up ? new Vector3(0f, floatHeight / steps, 0f) : new Vector3(0f, -floatHeight / steps, 0f);
        transform.position += move;
        count++;
        if (count == steps)
        {
            count = 0;
            up = !up;
        }
    }
}

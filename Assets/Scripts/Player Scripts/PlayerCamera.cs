using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerChar;
    public float minDistx, maxDistx;
    public float minDistz, maxDistz;
    public Vector3 offset;
    public float smoothspeed;
    Vector3 desiredPos;

    void Start()
    {
        desiredPos = playerChar.position + offset;
    }

    
    void Update()
    {
        desiredPos = playerChar.position + offset;

        if (desiredPos.x < minDistx)
        {
            desiredPos.x = minDistx;
        }
        else if (desiredPos.x > maxDistx)
        {
            desiredPos.x = maxDistx;
        }

        if (desiredPos.z < minDistz)
        {
            desiredPos.z = minDistz;
        }
        else if (desiredPos.z > maxDistz)
        {
            desiredPos.z = maxDistz;
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothspeed);
        transform.position = smoothPos;
    }
}

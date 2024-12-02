using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
    public float slowForce = 1;
    void Start()
    {
        if(slowForce <= 0)
            slowForce = 1;
    }

    void Update()
    {
        transform.Rotate(transform.right * Time.deltaTime / slowForce, Space.Self);
    }
}

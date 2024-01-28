using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limit2dVelocity : MonoBehaviour
{
    public float maxVelocity = 5f;
    Rigidbody2D rgbd2d;
    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rgbd2d.velocity = Vector2.ClampMagnitude(rgbd2d.velocity, maxVelocity);
    }
}

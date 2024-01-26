using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovementcontrol : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    ConstantForce2D cf2d;
    public float force = 5;
    public float torqueForce = 100;
    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        cf2d = GetComponent<ConstantForce2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, 0), Time.deltaTime);
    }

    public void MoveLeft()
    {
        cf2d.force = new Vector2 (-force, 0.0f);
        cf2d.torque = torqueForce;
    }

    public void MoveRight()
    {
        cf2d.force = new Vector2(force, 0.0f);
        cf2d.torque = -torqueForce;
    }
}

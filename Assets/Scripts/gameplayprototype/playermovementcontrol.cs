using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playermovementcontrol : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    ConstantForce2D cf2d;
    public float force = 5;
    public float torqueForce = 100;
    public PlayerFacialManager facialManager;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            facialManager.RemovePart();
        }
        else if(collision.gameObject.tag == "Collectible")
        {
            if (collision.gameObject.GetComponent<PartController>())
            {
                PartController part = collision.gameObject.GetComponent<PartController>();
                facialManager.AddPart(part.GetPartDetail());
                
            }
            else if (collision.gameObject.GetComponent<LimbController>())
            {
                LimbController limb = collision.gameObject.GetComponent<LimbController>();
                facialManager.AddPart(limb.GetLimbDetail());
            }

            Destroy(collision.gameObject);
        }
    }
}

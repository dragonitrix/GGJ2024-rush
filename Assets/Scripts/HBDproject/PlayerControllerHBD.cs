using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHBD : MonoBehaviour
{
    public bool isJumping = false;
    public bool isClimbing = false;
    public bool isDashing = false;
    public bool isDoubleJumping = false;
    Rigidbody2D rgbd2d;

    public float jumpForce = 5.0f;
    public float moveForce = 5.0f;
    public GameObject startPlatform;
    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    public void Move(int direction)
    {
        Vector2 currentvelo = rgbd2d.velocity;
        currentvelo.x = moveForce * direction;
        rgbd2d.velocity = currentvelo;
    }

    public void Jump(bool doubleJump = false)
    {
        if (startPlatform) Destroy(startPlatform);

        if (isJumping && isDoubleJumping) return;
        if (isDashing) return;
        if (doubleJump) isDoubleJumping = true;
        if (isClimbing)
        {
            isClimbing = false;
            if (!isJumping || (isJumping && !isDoubleJumping))
            {
                rgbd2d.AddForce((Vector2.up + (Vector2.right)) * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Vector2 currentvelo = rgbd2d.velocity;
            currentvelo.y = 0;
            rgbd2d.velocity = currentvelo;
            rgbd2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        if (!isJumping)
        {
            isJumping = true;
        }
        else if (isJumping && !isDoubleJumping)
        {
            isDoubleJumping = true;
        }
    }

    public IEnumerator jumpCorrection()
    {
        while (true)
        {

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        int currentMoveKey = 0;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            currentMoveKey = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            currentMoveKey = -1;
        }

        Move(currentMoveKey);

        Vector3 playerOnScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (playerOnScreenPos.x < 0 - gameObject.transform.localScale.x)
        {
            Vector3 rePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, transform.position.y, transform.position.z));
            rePosition.y = transform.position.y;
            rePosition.z = 0;
            transform.position = rePosition;
        }
        else if(playerOnScreenPos.x > Screen.width + gameObject.transform.localScale.x)
        {
            Vector3 rePosition = Camera.main.ScreenToWorldPoint(new Vector3(0, transform.position.y, transform.position.z));
            rePosition.y = transform.position.y;
            rePosition.z = 0;
            transform.position = rePosition;
        }
        if(playerOnScreenPos.y < 0 - gameObject.transform.localScale.y)
        {
            Vector3 rePosition = Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, Screen.height, transform.position.z));
            rePosition.x = transform.position.x;
            rePosition.z = 0;
            transform.position = rePosition;
        }
        else if(playerOnScreenPos.y > Screen.height + gameObject.transform.localScale.y)
        {
            Vector3 rePosition = Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, 0, transform.position.z));
            rePosition.x = transform.position.x;
            rePosition.z = 0;
            transform.position = rePosition;
        }
    }

    public void Dash()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            isClimbing = false;
            isDashing = false;
            isDoubleJumping = false;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            isJumping = false;
            isClimbing = true;
            isDashing = false;
            isDoubleJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "Wall")
        {
        }
    }
}

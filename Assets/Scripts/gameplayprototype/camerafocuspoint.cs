using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafocuspoint : MonoBehaviour
{
    public Transform player;
    public float cameraYOffset = -5;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, player.position.y, 0), Time.deltaTime);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset, -10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Reference")]
    public Transform transform_player;

    [Header("Settings")]
    public float cameraYOffset;
    public float swayInterval = 6f;
    public float swayDistance = 1f;

    float zDefault = -10f;

    Camera cam;

    Vector3 focusPoint;

    float elaspedY = 0f;
    
    [HideInInspector]
    public float sinYOffset;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        cam = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elaspedY += Mathf.PI * 2 * Time.deltaTime / swayInterval;
        if (elaspedY > 1000) elaspedY -= 1000;

        var idleMovement_distance = swayDistance;
        sinYOffset = Mathf.Sin(elaspedY) * idleMovement_distance;

        focusPoint = Vector3.Lerp(focusPoint, new Vector3(0, transform_player.position.y, 0), Time.deltaTime);
        transform.position = new Vector3(focusPoint.x, focusPoint.y + cameraYOffset + sinYOffset, zDefault);
    }
}

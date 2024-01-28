using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexController : MonoBehaviour
{

    public float lerpMultiplier = 1f;

    void Start(){
        var pos = CameraController.instance.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        var lerpVector = Vector3.Lerp(transform.position, CameraController.instance.transform.position, lerpMultiplier * Time.deltaTime);

        transform.position = new Vector3(lerpVector.x, lerpVector.y, transform.position.z);
    }
}

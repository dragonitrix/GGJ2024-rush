using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public Animator target;
    public bool alreadyTriggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(alreadyTriggered) return;

        if(collision.transform.name == "Player")
        {
            target.SetTrigger("Trigger");
            alreadyTriggered = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerScript player;
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision -- " + other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
        {
            player.isGrounded = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit collision -- " + other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
        {
            player.isGrounded = false;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
    }
}

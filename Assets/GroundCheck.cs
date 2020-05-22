using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerScript player;
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerScript>();
    }

    void Update()
    {
               
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.IsGrounded(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.IsGrounded(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject groundCheck;
    public bool isGrounded = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IsGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
}

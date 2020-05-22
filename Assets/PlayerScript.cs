using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float fallSpeed = -0.5f;
    public bool isGrounded = false;

    Vector3 movement = new Vector3();

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0;
        if (!isGrounded)
        {
            movement.y = fallSpeed;
        }
        rb.velocity = movement * movementSpeed;
    }

}

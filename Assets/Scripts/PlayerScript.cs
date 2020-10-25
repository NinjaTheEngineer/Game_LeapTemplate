using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject deadScriptObject;
    private DeadScreenScript deadScript;

    public GameManager gameManager;

    public float movementSpeed = 3.0f;
    public float fallSpeed = -0.5f;
    public bool isGrounded = false;

    public Vector3 movement = new Vector3();
    private Vector3 moveLeft;
    private Vector3 moveRight;

    private bool movingLeft = true;
    private bool movingRight = false;

    public MeshRenderer material;
    public bool secondTry = false;

    public float screenWidth;
    Rigidbody rb;
    void Start()
    {
        screenWidth = Screen.width;
        deadScript = deadScriptObject.GetComponent<DeadScreenScript>();
        rb = GetComponent<Rigidbody>();

        moveLeft = new Vector3(-1, transform.position.y, transform.position.z);
        moveRight = new Vector3(1, transform.position.y, transform.position.z);

        material = GetComponent<MeshRenderer>();

        material.material = gameManager.GetSelectedSkin();
    }

    private void FixedUpdate()
    {
        if (!gameManager.GameOnline())
        {
            if (movingLeft && !movingRight)
            {
                LerpLeft();
            }
            else
            {
                LerpRight();
            }
        }
        else
        {
            Movement();
            CheckForJump();
            if (isJumping)
            {
                jumpTime -= Time.smoothDeltaTime;
                if(jumpTime >= 0)
                {
                    Jump();
                }
                else
                {
                    isJumping = false;
                    jumpTime = 0.25f;
                }
            }
            if (transform.position.y < -0.5f)
            {
                if (secondTry)
                {
                    deadScript.GameOver();
                }
                else
                {
                    deadScript.PlayerIsDead();
                }
                secondTry = true;
            }
        }
    }

    public float jumpTime = 0.25f;

    public Ray rayPos;
    public Vector3 touchPos;

    [Range(-10, 10)]
    public float touchDistance;

    public float force;
    public bool isJumping = false;
    public bool secondJump = false;

    private void CheckForJump()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchIsTap = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchIsTap = false;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (touchIsTap && isGrounded) 
                {
                    isJumping = true;
                    secondJump = true;
                    jumpTime = 0.45f;
                    Debug.Log("JUMP JUMP JUMP");
                }
                if (touchIsTap && secondJump)
                {
                    isJumping = true;
                    secondJump = false;
                    jumpTime = 0.35f;
                    Debug.Log("DOUBLE JUMP");
                }
            }
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector3(0f, force, 0f), ForceMode.Force);
    }

    public bool touchIsTap = false;

    void Movement()
    {
        movement.y = 0;
        if (!isGrounded)
        {
            movement.y = fallSpeed;
        }
        if(secondTry && transform.position.y > 2.3f)
        {
            movement.y = fallSpeed / 2;
        }
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            rayPos = Camera.main.ScreenPointToRay(touch.position);
            touchPos = rayPos.GetPoint(touchDistance);

            if(Mathf.Abs(touchPos.x - transform.position.x) < 0.35f)
            {
                movement.x = 0;
            }
            else if (touchPos.x > transform.position.x)
            {
                movement.x = 1;
            }
            else if (touchPos.x < transform.position.x)
            {
                movement.x = -1;
            }
            else
            {
                movement.x = 0;
            }
            rb.velocity = movement * movementSpeed;

        }
        else
        {
            movement.x = 0;
        }
        rb.velocity = movement * movementSpeed;
        /*
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0;
        if (!isGrounded)
        {
            movement.y = fallSpeed;
        }else if(transform.position.y < -0.15f) {
            movement.y -= 0.01f;
        }
        rb.velocity = movement * movementSpeed;
        */
    }
    private void LerpLeft()
    {
        movement.x = -1;
        movement.y = 0;
        if (!isGrounded)
        {
            movement.y = fallSpeed;
        }
        else if (transform.position.y < -0.15f)
        {
            movement.y -= 0.01f;
        }
        rb.velocity = movement * (movementSpeed - 1.5f);
        if (transform.position.x <= -1)
        {
            movingLeft = false;
            movingRight = true;
        }
    }
    private void LerpRight()
    {
        movement.x = 1;
        movement.y = 0;
        if (!isGrounded)
        {
            movement.y = fallSpeed;
        }
        else if (transform.position.y < -0.15f)
        {
            movement.y -= 0.01f;
        }
        rb.velocity = movement * (movementSpeed - 1.5f);

        if (transform.position.x >= 1)
        {
            movingLeft = true;
            movingRight = false;
        }
    }

}

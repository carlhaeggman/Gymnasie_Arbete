using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float t;
    private bool timeStarted = false;
    private float timeElapsed;

    Vector3 cameraPosition;
    public Camera mainCamera;

    float basespeed;
    public float currentSpeed;
    public Rigidbody rb;
    private float horizontalX;
    private float horizontalY;

    public static float globalGravity = -9.82f;
    public float gravityScale = 1.0f;

    public Transform cameraHolder;

    private float slideTime;
    private bool canSlide;
    
    void Start()
    {
        currentSpeed = 500;
        basespeed = 500;
        slideTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        MovementSlide();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        horizontalX = Input.GetAxisRaw("Horizontal");
        horizontalY = Input.GetAxisRaw("Vertical");

        rb.AddRelativeForce(Time.deltaTime * horizontalX * currentSpeed, 0 * Time.deltaTime, Time.deltaTime * horizontalY * currentSpeed, ForceMode.VelocityChange);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 800f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = 500;
        }
    }

    void MovementSlide()
    {

        if (!timeStarted)
        {
            timeElapsed = Time.time;
        }
        float t =+ Time.time - timeElapsed;
        if (Input.GetKeyDown(KeyCode.C) && canSlide == true)
        {
            canSlide = false;
            timeStarted = true;
            currentSpeed = Mathf.Lerp(basespeed * 1.5f, basespeed * 0.6f, t / slideTime);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            canSlide = true;
            currentSpeed = basespeed;
        }


    }

  
    /*
    void MovementSlide()
    {
        if (!timeStarted)
        {
            timeElapsed = Time.time;
        }
        float t =+ Time.time - timeElapsed;
        

            slideDistance = Mathf.Lerp(1, 0.2f, t / slideTime);
            currentSpeed = basespeed * (slideValue.Evaluate(slideDistance) * slideMultiplier);
        timeStarted = true;
    }*/
}



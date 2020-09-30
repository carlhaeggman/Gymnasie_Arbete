using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

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

    public AnimationCurve slideValue;
    private float slideDistance;
    public float slideMultiplier;
    public bool canSlide;
    
    void Start()
    {
        currentSpeed = 500;
        basespeed = 500;
        canSlide = true;
    }

    // Update is called once per frame
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

        if (Input.GetKey(KeyCode.C) && canSlide == true && horizontalY > 0)
        {
            MovementSlide();
            canSlide = false;
         
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            canSlide = true;
            currentSpeed = basespeed;
        }
        
    }

    void MovementSlide()
    {
            slideDistance = Mathf.Lerp(1, 0.2f, Time.deltaTime * 2);
            currentSpeed = basespeed * (slideValue.Evaluate(slideDistance) * slideMultiplier);
    }
}



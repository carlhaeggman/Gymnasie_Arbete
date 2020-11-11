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

    public float currentSpeed;
    public Rigidbody rb;
    private float horizontalX;
    private float horizontalY;
    bool canMove;

    public static float globalGravity = -9.82f;
    public float gravityScale = 1.0f;

    public Transform cameraHolder;

<<<<<<< HEAD
    Rigidbody rig;
    CapsuleCollider coll;
    float originalHeight;
    public float reducedHeight;
    public float slideLength;

    public float mouseSensitivity;
    private float xRotation;


=======
    private float slideTime;
    private bool canSlide;
>>>>>>> 4ca20df804490294faf2cb1552fbe903848e5598
    
    void Start()
    {
        slideLength = 0.05f;
        coll = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        originalHeight = coll.height;

        canMove = true;
        currentSpeed = 500;
<<<<<<< HEAD
        mouseSensitivity = 200f;
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
=======
        basespeed = 500;
        slideTime = 3f;
>>>>>>> 4ca20df804490294faf2cb1552fbe903848e5598
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
<<<<<<< HEAD
    void Update()
=======

    void Movement()
>>>>>>> 4ca20df804490294faf2cb1552fbe903848e5598
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("sup");
            Sliding();
            canMove = false;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            GoUp();
            canMove = true;
            
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 900f;
            slideLength = 0.25f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = 500;
            slideLength = 0.05f;
        }
    }

<<<<<<< HEAD
        LookAround();
    }


    void Movement()
    {
        if(canMove == true)
=======
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
>>>>>>> 4ca20df804490294faf2cb1552fbe903848e5598
        {
            horizontalX = Input.GetAxisRaw("Horizontal");
            horizontalY = Input.GetAxisRaw("Vertical");

            rb.AddRelativeForce(Time.deltaTime * horizontalX * currentSpeed, 0 * Time.deltaTime, Time.deltaTime * horizontalY * currentSpeed, ForceMode.VelocityChange);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
<<<<<<< HEAD
    }

    private void Sliding()
    {

        coll.height = reducedHeight;
        rig.AddForce(transform.forward * slideLength, ForceMode.VelocityChange);

    }

    private void GoUp()
    {
        coll.height = Mathf.Lerp(coll.height, originalHeight, Time.time/3);
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
=======


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
>>>>>>> 4ca20df804490294faf2cb1552fbe903848e5598
}



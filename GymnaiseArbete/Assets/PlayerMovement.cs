using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 cameraPosition;
    public Camera mainCamera;

    public float currentSpeed;
    public Rigidbody rb;
    private float horizontalX;
    private float horizontalY;
    bool canMove;

    public Transform cameraHolder;

    Rigidbody rig;
    CapsuleCollider col;
    float originalHeight;
    private float reducedHeight;
    private float slideLength;
    bool timeStarted = false;
    private float timeElsapsed;
    private float getUpDuration;


    private float mouseSensitivity;
    private float xRotation;

    public LayerMask groundLayers;
    private float jumpForce;

    
    void Start()
    {
        getUpDuration = 1f;
        reducedHeight = 0.8f;
        jumpForce = 5f;
        slideLength = 0.7f;

        rig = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        originalHeight = col.height;

        canMove = true;
        currentSpeed = 350;
        mouseSensitivity = 200f;
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //Kör "Movement" funktionen
        Movement();

        Physics.gravity = new Vector3(0, -13F, 0);


    }
    void Update()
    {

        Debug.Log(canMove);

        //Kör "Sliding" funktionen om man håller inne C medan W hålls nere
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.W))
        {
            Sliding();
            if(rb.velocity.x > 0.5)
            {
                canMove = false;
            }
           
        }
        //Om man släpper up "C" körs "GoUp" funktionen
        else if (Input.GetKeyUp(KeyCode.C))
        {
            GetUp();
            canMove = true;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 500f;
            slideLength = 2.3f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = 350;
            slideLength = 0.1f;
        }

        Jump();

        LookAround();
    }


    void Movement()
    {
        if (canMove == true)
        {
            horizontalX = Input.GetAxisRaw("Horizontal");
            horizontalY = Input.GetAxisRaw("Vertical");

            rb.AddRelativeForce(Time.deltaTime * horizontalX * currentSpeed, 0 * Time.deltaTime, Time.deltaTime * horizontalY * currentSpeed, ForceMode.VelocityChange);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void Sliding()
    {

        col.height = reducedHeight;
        rig.AddForce(transform.forward * slideLength, ForceMode.VelocityChange);

        if(rig.velocity.x < 0.5)
        {
            canMove = true;
            currentSpeed = 10;
            Debug.Log(canMove);
        }

    }

    private void GetUp()
    {
        
       
        if (!timeStarted)
        {
            timeElsapsed = Time.time;
            timeStarted = true;
        }

        float t =+ Time.time - timeElsapsed;

        col.height = Mathf.Lerp(col.height, originalHeight, Time.time / getUpDuration);
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

    private void Jump()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y,col.bounds.center.z), col.radius * .9f, groundLayers);
       
    }
}



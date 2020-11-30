using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Kamera variabler och komponenter
    public Camera mainCamera;
    public Transform cameraHolder;

    //Generella variabler och komponenter
    public float currentSpeed;
    public Rigidbody rb;
    //private float horizontalX;
    //private float horizontalZ;
    CapsuleCollider col;
    float originalHeight;
    private float reducedHeight;
    public LayerMask ignorePlayer;

    //Sliding variabler
    private float slideLength;
    private bool startedSliding;

    //"Titta runt" variabler
    private float mouseSensitivity;
    private float sensMultiplier;
    private float xRotation;

    //"Jump" variabler
    public LayerMask groundLayers;
    private float jumpForce;

    public Slider sensSlider;
    public Text sensText;

    //Bools för de olike "State" funktionerna
    bool canSlide;
    bool canMove;
    bool willWalk;

    bool haveGotUp;

    float walkSpeed = 8;
    float runSpeed = 13;

    void Start()
    {
        startedSliding = false;
        reducedHeight = 0.8f;
        slideLength = 0.7f;

        jumpForce = 8f;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        originalHeight = col.height;

        currentSpeed = walkSpeed;
        mouseSensitivity = 20f;
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;

        haveGotUp = true;
        canMove = true;
        willWalk = true;
        canSlide = true;
    }

    void FixedUpdate()
    {
        //Kör "Movement" funktionen
        if(willWalk)
        {
            MoveState();
        }
        Physics.gravity = new Vector3(0, -18F, 0);
    }
    void Update()
    {
        sensText.text = mouseSensitivity.ToString();
        StateManager();
        
        Jump();

        LookAround();

        if (Input.GetKeyDown(KeyCode.P))
        {
            mouseSensitivity += 1f;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            mouseSensitivity -= 1f;
        }
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 10 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 10* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 1.05f, groundLayers);

    }
    //Funktion som får spelaren att hoppa genom att lägga till kraft uppåt på rigidbodyn
    private void Jump()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void MoveState()
    {
        var horizontalZ = Input.GetAxisRaw("Vertical");
        var horizontalX = Input.GetAxisRaw("Horizontal");

        var relativeForce = new Vector3(horizontalX * Time.deltaTime, 0 *Time.deltaTime, horizontalZ * Time.deltaTime);
        if (relativeForce.magnitude > 1)
        {
            relativeForce.Normalize();
        }

        rb.AddRelativeForce(relativeForce.normalized * currentSpeed, ForceMode.VelocityChange);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    private void SlideState()
    {
        col.height = reducedHeight;
        rb.AddForce(transform.forward * slideLength, ForceMode.VelocityChange);
    }

    private void StateManager()
    {
        if (startedSliding == true)
        {
            canMove = false;
        }
        var speed = rb.velocity.magnitude;

        //Kör "Sliding" funktionen om man håller inne C medan W hålls nere
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.W) && canSlide == true && isGrounded() || Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && canSlide == true && isGrounded())
        {
            
            SlideState();
            if (startedSliding == false)
            {
                haveGotUp = false;
                canMove = false;
                startedSliding = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            RaycastHit roofDetector;
            if (Physics.Raycast(transform.position, transform.up, out roofDetector, 10, ~ignorePlayer))
            {
                float distanceToRoof = roofDetector.distance;
                if (distanceToRoof <= originalHeight-0.1f)
                {
                    currentSpeed = 3.5f;
                    col.height = reducedHeight;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed = runSpeed;
                }
                else
                {
                    currentSpeed = walkSpeed;
                }
                col.height = originalHeight;
                haveGotUp = true;
            }
            canMove = true;
            startedSliding = false;
        }
        
        if(haveGotUp == false && startedSliding == false)
        {
            RaycastHit roofDetector;
          
            if(Physics.Raycast(transform.position, transform.up, out roofDetector, 10, ~ignorePlayer))
            {
               
                if (roofDetector.distance <= originalHeight -0.1f)
                {
                    col.height = reducedHeight;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        currentSpeed = runSpeed;
                    }
                    else
                    {
                        currentSpeed = walkSpeed;
                    }
                }
            }
            else
            {
                haveGotUp = true;
                col.height = originalHeight;
                currentSpeed = walkSpeed;
            }
        }
        if (canMove == true)
        {
            willWalk = true;
        }
        else
        {
            willWalk = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            slideLength = 2.85f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            slideLength = 0.1f;
 
            if (startedSliding == false)
            {
                canMove = true;
            }
        }
    }
}



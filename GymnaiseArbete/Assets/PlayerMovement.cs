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

    public static float globalGravity = -9.82f;
    public float gravityScale = 1.0f;

    public Transform cameraHolder;

    Rigidbody rig;
    CapsuleCollider coll;
    float originalHeight;
    public float reducedHeight;
    public float slideLength;

    public float mouseSensitivity;
    private float xRotation;


    
    void Start()
    {
        slideLength = 0.05f;
        coll = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        originalHeight = coll.height;

        canMove = true;
        currentSpeed = 500;
        mouseSensitivity = 200f;
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Update()
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

        LookAround();
    }


    void Movement()
    {
        if(canMove == true)
        {
            horizontalX = Input.GetAxisRaw("Horizontal");
            horizontalY = Input.GetAxisRaw("Vertical");

            rb.AddRelativeForce(Time.deltaTime * horizontalX * currentSpeed, 0 * Time.deltaTime, Time.deltaTime * horizontalY * currentSpeed, ForceMode.VelocityChange);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
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
}



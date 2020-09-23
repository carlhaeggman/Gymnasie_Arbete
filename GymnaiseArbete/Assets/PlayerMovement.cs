using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody rb;
    private float horizontalX;
    private float horizontalY;


    // Start is called before the first frame update
    void Start()
    {
        
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
        var movementDirection = rb.velocity = new Vector3(horizontalX, 0, horizontalY) * speed * Time.deltaTime;
     
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPlayer : MonoBehaviour
{
//For Viewpoint
    public float xSensitivity;
    public float ySensitivity;
    public Transform cameraTransform;
    
    float xRotation;
    float yRotation;

    private float maxCameraViewAngle = 10f;

//For character movement
    public float speed;
    Vector3 movementDirection;

    private Rigidbody rb;
    public float jumpForce;
    bool isGrounded;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks cursor to center of game view during play (hit esc to exit)
        rb=GetComponent<Rigidbody>(); 
        isGrounded=true;

    }

    void Update()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(movementDirection) * speed * Time.deltaTime);
        
        CameraView();
        Jump();
        PlayerMovement();

    }

    public void CameraView()
    {
        float deltaX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRotation += deltaX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        
        float deltaY = Input.GetAxis("Mouse Y") * (-ySensitivity) * Time.deltaTime;
        xRotation -= deltaY;
        xRotation = Mathf.Clamp(xRotation, -maxCameraViewAngle, maxCameraViewAngle);
       
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation,0f,0f);
       // Vector3 newRotation = cameraTransform.transform.rotation.eulerAngles + new Vector3(deltaY, 0f, 0f); // euler angles turns quaternion into vector3

    }


    public void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movementDirection = transform.right * horizontal + transform.forward * vertical;

        rb.MovePosition(rb.position + movementDirection * speed * Time.deltaTime);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
}
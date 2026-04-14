using System;
using UnityEngine;

public class FirstPersonPlayer : MonoBehaviour
{
//For Viewpoint
    public float xSensitivity;
    public float ySensitivity;
    public Transform cameraTransform;
    
    float xRotation;
    float yRotation;

    private float maxCameraViewAngle = 40f;

//For character movement
    public float speed;
    Vector3 movementDirection;
    private CharacterController characterController;
    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;
    public float gravity;
    public LayerMask groundMask;
    public float groundDistance;

    public event EventHandler MissingPersonFound;
    private bool canMove = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks cursor to center of game view during play (hit esc to exit)
        isGrounded=true;
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {    
        if (canMove)
        {
            CameraView();
            PlayerMovement();
        }
     
    }

    public void CameraView()
    {
        float deltaX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRotation += deltaX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        float deltaY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        xRotation -= deltaY;
        xRotation = Mathf.Clamp(xRotation, -maxCameraViewAngle, maxCameraViewAngle);
       
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation,0f,0f);
       

    }


    public void PlayerMovement()
    {
       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

       if (isGrounded && velocity.y < 0)
       {
           velocity.y = -2f;
       }
       
       float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        
        Vector3 movementDirection = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(movementDirection * speed * Time.deltaTime);

       velocity.y += gravity * Time.deltaTime;
       characterController.Move(velocity * Time.deltaTime);

      
        
    }
    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }


    private void OnControllerColliderHit (ControllerColliderHit hit)
    {
       

        if (hit.gameObject.CompareTag("MissingPerson"))
        {
            MissingPersonFound?.Invoke(this, EventArgs.Empty);
        }
    }
}
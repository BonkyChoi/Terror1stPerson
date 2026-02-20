using UnityEngine;

public class PlayerMovementV : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Mouse")]
    public Transform cameraTransform;
    public float mouseSensitivity = 80f;
    public float maxLookUp = 60f;
    public float maxLookDown = -60f;

    [Header("Agacharse")]
    public float crouchHeight = 1f;
    public float standingHeight = 1.8f;
    public float crouchSpeed = 2f;

    private CharacterController controller;
    private float xRotation = 0f;
    private float yVelocity = 0f;
    private bool isCrouching = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Crouch();
        Move();
        Look();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        if (isCrouching) currentSpeed = crouchSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
            
            if (!isCrouching && Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, maxLookDown, maxLookUp);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            controller.height = crouchHeight;
            isCrouching = true;
        }
        else
        {
            controller.height = standingHeight;
            isCrouching = false;
        }
        
        cameraTransform.localPosition = new Vector3(0, controller.height - 0.2f, 0);
    }
}
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

    [Header("Agarre")]
    public Transform holdPointRight;
    public Transform holdPointLeft;
    public Transform holdPointHeavy;

    public float grabDistance = 3f;
    public float throwForce = 8f;
    public float heavyThrowForce = 4f;

    [Header("Highlight")]
    public Material outlineMaterial;
    
    [Header("Empuje")]
    public float pushForce = 5f;
    public float pushDistance = 2f;

    private CharacterController controller;
    private float xRotation = 0f;
    private float yVelocity = 0f;
    private bool isCrouching = false;
    
    private GameObject rightHandObject;
    private GameObject leftHandObject;
    private GameObject heavyObject;

    private Rigidbody rightRb;
    private Rigidbody leftRb;
    private Rigidbody heavyRb;
    
    private GameObject highlightedObject;
    private Renderer highlightedRenderer;
    private Material originalMaterial;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Move();
        Look();
        Crouch();
        HandleGrab();
        HandleThrow();
        CheckHighlight();
        HandlePush();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        if (isCrouching) currentSpeed = crouchSpeed;

        Vector3 move = (transform.right * x + transform.forward * z) * currentSpeed;

        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -1f;

            if (!isCrouching && Input.GetKeyDown(KeyCode.Space))
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * Time.deltaTime);
    }
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

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

        controller.center = new Vector3(0, controller.height / 2f, 0);
        cameraTransform.localPosition = new Vector3(0, controller.height - 0.2f, 0);
    }
    void HandleGrab()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Lanzable"))
                GrabSmall(hit.collider.gameObject);

            if (hit.collider.CompareTag("Pesado"))
                GrabHeavy(hit.collider.gameObject);
        }
    }
    void GrabSmall(GameObject obj)
    {
        if (heavyObject != null) return;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rightHandObject == null)
        {
            rightHandObject = obj;
            rightRb = rb;

            rb.isKinematic = true;
            obj.transform.SetParent(holdPointRight);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
        else if (leftHandObject == null)
        {
            leftHandObject = obj;
            leftRb = rb;

            rb.isKinematic = true;
            obj.transform.SetParent(holdPointLeft);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
    void GrabHeavy(GameObject obj)
    {
        if (rightHandObject != null || leftHandObject != null || heavyObject != null) return;

        heavyObject = obj;
        heavyRb = obj.GetComponent<Rigidbody>();
        if (heavyRb == null) return;

        heavyRb.isKinematic = true;
        obj.transform.SetParent(holdPointHeavy);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
    void HandleThrow()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        ThrowObject();
    }
    void ThrowObject()
    {
        if (rightHandObject != null)
        {
            rightHandObject.transform.SetParent(null);
            rightRb.isKinematic = false;
            rightRb.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);
            rightHandObject = null;
        }
        else if (leftHandObject != null)
        {
            leftHandObject.transform.SetParent(null);
            leftRb.isKinematic = false;
            leftRb.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);
            leftHandObject = null;
        }
        else if (heavyObject != null)
        {
            heavyObject.transform.SetParent(null);
            heavyRb.isKinematic = false;
            heavyRb.AddForce(cameraTransform.forward * heavyThrowForce, ForceMode.Impulse);
            heavyObject = null;
        }
    }
    void CheckHighlight()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Lanzable") || hit.collider.CompareTag("Pesado"))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null && hit.collider.gameObject != highlightedObject)
                {
                    RemoveHighlight();

                    highlightedObject = hit.collider.gameObject;
                    highlightedRenderer = rend;
                    originalMaterial = rend.material;

                    rend.material = outlineMaterial;
                }
                return;
            }
        }
        RemoveHighlight();
    }
    void RemoveHighlight()
    {
        if (highlightedObject != null)
        {
            highlightedRenderer.material = originalMaterial;
            highlightedObject = null;
            highlightedRenderer = null;
        }
    }
    void HandlePush()
    {
        if (!Input.GetKeyDown(KeyCode.F)) return;
        
        if (rightHandObject != null || leftHandObject != null || heavyObject != null)
            return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pushDistance))
        {
            if (hit.collider.CompareTag("Empujable"))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDir = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
                    rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}
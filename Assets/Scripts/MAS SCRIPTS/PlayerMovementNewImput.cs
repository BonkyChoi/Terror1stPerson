using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovementNewImput : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    
    [Header("Trigger esfera")]
    public SphereCollider triggerSphere;
    public float moveRadius = 1.5f;
    public float runRadius = 2.2f;
    public float crouchRadius = 1f;

    [Header("Mouse")]
    public Transform cameraTransform;
    public float mouseSensitivity = 80f;
    public float maxLookUp = 60f;
    public float maxLookDown = -60f;

    [Header("Agacharse")]
    public float crouchHeight = 1f;
    public float standingHeight = 1.8f;
    public float crouchSpeed = 2f;
    
    [Header("Detección techo")]
    public Transform headCheck;
    public float headRadius = 0.25f;
    public LayerMask ceilingMask;

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
    public float pushCooldown = 2f;

    [Header("Fusibles")]
    public List<GameObject> fusibles = new List<GameObject>();

    private CharacterController controller;
    private IA_Player input;

    private float xRotation = 0f;
    private float yVelocity = 0f;
    private bool isCrouching = false;
    private float currentHeight;

    private GameObject rightHandObject;
    private GameObject leftHandObject;
    private GameObject heavyObject;

    private Rigidbody rightRb;
    private Rigidbody leftRb;
    private Rigidbody heavyRb;

    private GameObject highlightedObject;
    private Renderer highlightedRenderer;
    private Material originalMaterial;

    private float pushTimer = 0f;

    void Awake()
    {
        input = new IA_Player();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHeight = standingHeight;
    }

    void Update()
    {
        Move();
        Look();
        Crouch();
        HandleInput();
        CheckHighlight();

        if (pushTimer > 0)
            pushTimer -= Time.deltaTime;
    }

    void HandleInput()
    {
        if (input.Player.Interact.triggered)
            TryInteract();

        if (input.Player.Throw.triggered)
            ThrowObject();

        if (input.Player.Push.triggered)
            HandlePush();
    }

    void Move()
    {
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        float x = moveInput.x;
        float z = moveInput.y;

        float currentSpeed = input.Player.Run.IsPressed() ? runSpeed : walkSpeed;
        if (isCrouching) currentSpeed = crouchSpeed;

        Vector3 move = (transform.right * x + transform.forward * z) * currentSpeed;

        UpdateTriggerRadius(x, z);

        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -1f;

            if (!isCrouching && input.Player.Jump.triggered)
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * Time.deltaTime);
    }

    void Look()
    {
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, maxLookDown, maxLookUp);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    bool HayTecho()
    {
        return Physics.CheckSphere(headCheck.position, headRadius, ceilingMask);
    }

    void Crouch()
    {
        bool quiereAgacharse = input.Player.Crouch.IsPressed();

        if (!quiereAgacharse && HayTecho())
            quiereAgacharse = true;

        isCrouching = quiereAgacharse;

        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * 10f);

        controller.height = currentHeight;
        controller.center = new Vector3(0, currentHeight / 2f, 0);

        Vector3 camPos = cameraTransform.localPosition;
        float targetCamY = currentHeight - 0.2f;
        camPos.y = Mathf.Lerp(camPos.y, targetCamY, Time.deltaTime * 10f);
        cameraTransform.localPosition = camPos;
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Lanzable") || hit.collider.CompareTag("Fusible"))
                GrabSmall(hit.collider.gameObject);

            else if (hit.collider.CompareTag("Pesado"))
                GrabHeavy(hit.collider.gameObject);
        }
    }

    void GrabSmall(GameObject obj)
    {
        obj.layer = LayerMask.NameToLayer("Se ve");
        if (heavyObject != null) return;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rightHandObject == null)
        {
            rightHandObject = obj;
            rightRb = rb;
        }
        else if (leftHandObject == null)
        {
            leftHandObject = obj;
            leftRb = rb;
        }
        else return;

        rb.isKinematic = true;
        obj.transform.SetParent(rightHandObject == obj ? holdPointRight : holdPointLeft);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        if (obj.CompareTag("Fusible") && !fusibles.Contains(obj))
            fusibles.Add(obj);
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

    void ThrowObject()
    {
        if (rightHandObject != null)
        {
            ReleaseAndThrow(rightHandObject, rightRb, throwForce);
            rightHandObject = null;
        }
        else if (leftHandObject != null)
        {
            ReleaseAndThrow(leftHandObject, leftRb, throwForce);
            leftHandObject = null;
        }
        else if (heavyObject != null)
        {
            ReleaseAndThrow(heavyObject, heavyRb, heavyThrowForce);
            heavyObject = null;
        }
    }

    void ReleaseAndThrow(GameObject obj, Rigidbody rb, float force)
    {
        if (obj.CompareTag("Fusible"))
            fusibles.Remove(obj);

        obj.transform.SetParent(null);
        obj.layer = LayerMask.NameToLayer("Default");
        rb.isKinematic = false;
        rb.AddForce(cameraTransform.forward * force, ForceMode.Impulse);
    }

    void CheckHighlight()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj == rightHandObject || obj == leftHandObject || obj == heavyObject)
            {
                RemoveHighlight();
                return;
            }

            if (hit.collider.CompareTag("Lanzable") || hit.collider.CompareTag("Pesado") || hit.collider.CompareTag("Fusible"))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();

                if (rend != null && obj != highlightedObject)
                {
                    RemoveHighlight();

                    highlightedObject = obj;
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
        }
    }

    void HandlePush()
    {
        if (pushTimer > 0) return;

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
                    Vector3 dir = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
                    rb.AddForce(dir * pushForce, ForceMode.Impulse);
                    pushTimer = pushCooldown;
                }
            }
        }
    }

    void UpdateTriggerRadius(float x, float z)
    {
        if (triggerSphere == null) return;

        bool isMoving = x != 0 || z != 0;
        bool isRunning = input.Player.Run.IsPressed() && isMoving && !isCrouching;

        if (isCrouching)
            triggerSphere.radius = crouchRadius;
        else if (isRunning)
            triggerSphere.radius = runRadius;
        else if (isMoving)
            triggerSphere.radius = moveRadius;
    }
}

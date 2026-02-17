using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //inputs
    private float x;

    private float y;

    private float z;



    //input-keys
    [SerializeField] private KeyCode jump = KeyCode.Space;

    //forces
    [SerializeField] private float movementForce; //10
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;

    //references
    private Rigidbody rb;
    [SerializeField] private Transform cam;

    //states
    private string state = "movement";

    //can
    [SerializeField] public bool canJump = false;
    private bool doubleJump;
    private bool applyGrav = false;

    [SerializeField] private bool canDoublejump;


    //bool
    private bool hasDoubleJumped = false;
    private bool canMakeDoubleJump = false;
    private int counterScene;
    private bool isJumping;
    
    //sounds
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioJump;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //canJump = true;
        //doubleJump = false;
    }

    private void Update()
    {
        Movement();

        if (CanIJump()&&Input.GetKeyDown(jump))
        {
            //Jump();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(audioJump);
            canJump = true;
        }else if(Input.GetKeyDown(jump)&&canJump && canDoublejump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(audioJump);
            canJump = false;
        }
        else
        {
            
        }

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CanIJump())
            {
                Debug.Log("Puedo");
            }
            else
            {
                Debug.Log("No");
            }
        }*/



    }

    private void DoubleJump()
    {
      //  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      //  doubleJump = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Force);
       // if (!canJump)
        //{
            
        //}
    }

    private void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");


        Vector3 moveDir =
            cam.transform.right * x + Vector3.ProjectOnPlane(cam.transform.forward * z,Vector3.up); //transform hace q mires en cuestion al jugador
        rb.linearVelocity += moveDir * movementForce;


    }

    private void Jump()
    {
        Debug.Log("Realizo un salto");

        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
       /* if (Input.GetKeyDown(jump))
        {
            Debug.Log("Quiero hacer un doble ssalto");
            DoubleJump();
        }*/
    }

    public void Knockback(Vector3 knockDirection, float damage)
    {
        //rb.isKinematic = false;
        //rb.AddForce(knockDirection * damage, ForceMode.Impulse);
    }

    public int CounterScene(int countersScene)
    {
        counterScene++;
        if (counterScene >= 2) canMakeDoubleJump = true;
        return counterScene;
    }

    // private void CanDoubleJump()
    // {
    //     if ()
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Path")
        {
           // canJump = true;
        }
        //else canJump = false;
    }

    public bool CanIJump()
    {
        RaycastHit rch;
        bool hit = Physics.SphereCast(transform.position - Vector3.up * 0.4f,  0.5f, Vector3.down, out rch,
             0.25f);
        if (hit)
        {
            if (Vector3.Angle(rch.normal, Vector3.up) < 45f)
            {
                return true;
            }

            return false;
        }
        else
        {
            return false;
        }
        
    }

}

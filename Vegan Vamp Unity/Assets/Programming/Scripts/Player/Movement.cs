using UnityEngine;

public class Movement : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] Transform orientTransform;
    [SerializeField] LayerMask groundLayer;

    Rigidbody rb;
    Animator animator;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Walking Settings")]
    [SerializeField] float playerHeight;
    [SerializeField] public float moveSpeed;
    [SerializeField] float groundDrag;

    [Header ("Jumping Settings")]
    [SerializeField] public float jumpForce;
    [SerializeField] float airMultiplier;
    
    //jumping
    bool grounded;

    //moving
    float verticalInput;
    float horizontalInput;
    Vector3 moveDirection;

    StatsManager selfStats;

    RaycastHit hit;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void GetInputs()
    {
        if (selfStats.ice[StatsConst.SELF_INTENSITY] <= 0)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Jump") && grounded && !selfStats.dead)
            {
                Jump();
            }
        }
    }

    void MovePlayer()
    {
        if (!selfStats.dead)
        {
            moveDirection = orientTransform.forward * verticalInput + orientTransform.right * horizontalInput;

            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * selfStats.speedMultiplier) * 10, ForceMode.Force);
            }

            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * selfStats.speedMultiplier) * airMultiplier * 10, ForceMode.Force);
            }

            if (rb.velocity.magnitude > 2f && grounded)
            {
                animator.SetBool("Running", true);
            }

            else
            {
                animator.SetBool("Running", false);
            }
        }
    }

    void CapVelocity()
    {
        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalVel.magnitude > (moveSpeed * selfStats.speedMultiplier))
        {
            Vector3 cappedVel = horizontalVel.normalized * moveSpeed;
            rb.velocity = new Vector3(cappedVel.x, rb.velocity.y, cappedVel.z);
        } 
    }

    void CheckIfGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.2f, groundLayer);

        if (grounded)
        {
            rb.drag = groundDrag;
            animator.SetBool("Falling", false);
        }

        else
        {
            rb.drag = 0;
            animator.SetBool("Falling", true);
        }
    }

    void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //play animation
        animator.SetTrigger("Jump");
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        selfStats = GetComponent<StatsManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();

        GetInputs();

        //Check if on ground
        CheckIfGrounded();

        //Max velocity cap
        CapVelocity();
    }

    #endregion
    //========================


}

using UnityEngine;

public class Movement : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] Transform orientTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask groundLayer;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Walking Settings")]
    [SerializeField] float playerHeight;
    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;

    [Header ("Jumping Settings")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    
    //jumping
    bool grounded;
    bool readyToJump = true;

    //moving
    Vector3 moveDirection;

    RaycastHit hit;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region



    public void MovePlayer(float verticalInput, float horizontalInput)
    {
        moveDirection = orientTransform.forward * verticalInput + orientTransform.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10, ForceMode.Force);
        }   
    }

    void CapVelocity()
    {
        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalVel.magnitude > moveSpeed)
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
        }

        else
        {
            rb.drag = 0;
        }
    }

    public void Jump()
    {
        if (readyToJump && grounded)
        {
            //reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Invoke("Reset Jump", jumpCooldown);
        }
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        //Check if on ground
        CheckIfGrounded();

        //Max velocity cap
        CapVelocity();
    }

    #endregion
    //========================


}

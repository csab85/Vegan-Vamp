using UnityEngine;

public class Movement : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header("Player Components")]
    [SerializeField][Tooltip ("Rigid Body")] Rigidbody rb;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void VerticalWalk(float speed)
    {
        rb.AddForce(transform.forward * (speed/10), ForceMode.VelocityChange);
    }

    void HorizontalWalk(float speed)
    {
        rb.AddForce(transform.right * (speed/10), ForceMode.VelocityChange);
    }

    void Decelarate(float verticalInput, float horizontalInput)
    {
        if (verticalInput == 0)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(rb.velocity.x, rb.velocity.y, 0), 0.1f);
        }

        if (horizontalInput == 0)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, rb.velocity.y, rb.velocity.z), 0.1f);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    //Start
    void Start()
    {

    }

    //Update
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            VerticalWalk(Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            HorizontalWalk(Input.GetAxis("Horizontal"));
        }

        if (Input.GetAxis("Vertical") == 0 | Input.GetAxis("Horizontal") == 0)
        {
            Decelarate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        }
    }

    #endregion
    //========================


}

using System.Linq;
using Unity.VisualScripting;
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

    [Header("Settings")]
    public float speed;
    public  Vector2 maxVelocity;
    public float drag;    
    public float jumpForce;

    [SerializeField][Tooltip ("How much to ratio the horizontal max velocity ")] float horizontalMaxVRatio;

    //Tags that count as floor
    [SerializeField][Tooltip ("Tags of objects that count as floor")] string[] floorTags;

    //states
    public bool touchingFloor = true;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    /// <summary>
    /// Caps velocity x and y to the maxVelocity Vector2 value
    /// </summary>
    /// <param name="horizontal">Default false. If true, ratios max speed</param>
    /// <returns>True if capping any velocity, False if not</returns>
    bool CapMaxVelocity(bool horizontal = false)
    {
        bool capping = false;
        float mvX = 0;
        float mvY = 0;

        if (horizontal)
        {
            mvX = maxVelocity.x / horizontalMaxVRatio;
            mvY = maxVelocity.y / horizontalMaxVRatio;
        }

        else
        {
            mvX = maxVelocity.x;
            mvY = maxVelocity.y;
        }

        if (Mathf.Abs(rb.velocity.x) >= mvX)
        {
            float velocitySign = Mathf.Sign(rb.velocity.x);

            rb.velocity = new Vector3(mvX * velocitySign, rb.velocity.y, rb.velocity.z);
            capping = true;
        }

        if (Mathf.Abs(rb.velocity.z) >= mvY)
        {
            float velocitySign = Mathf.Sign(rb.velocity.z);

            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, mvY * velocitySign);
            capping = true;
        }

        return capping;
    }

    /// <summary>
    /// Makes the player move forward or backwards
    /// </summary>
    /// <param name="sign">Positive number to go forward, negative to backwards</param>
    void VerticalWalk(float sign)
    {       
        if (!CapMaxVelocity())
        {
            float direction = Mathf.Sign(sign);

            Vector3 force = transform.forward * direction * speed * Time.deltaTime;
            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Makes the player move right or left
    /// </summary>
    /// <param name="sign">Positive number to go right, negative to go lef</param>
    void HorizontalWalk(float sign)
    {
        if (!CapMaxVelocity(horizontal : true))
        {
            float direction = Mathf.Sign(sign);

            Vector3 force = transform.right * direction * speed * Time.deltaTime;
            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Turns drag to the designated value if no movement button is pressed and speed > 0; If not drag is 0
    /// </summary>
    void Decelerate()
    {
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            if (rb.velocity.x > 0 | rb.velocity.z > 0)
            {
                rb.drag = drag;
            }
        }

        else
        {
            rb.drag = 0;
        }
    }
    
    /// <summary>
    /// If touching floor, adds applies force up on the player
    /// </summary>
    void Jump()
    {
        if (touchingFloor)
        {
            rb.AddForce(transform.up * jumpForce * Time.deltaTime * 10, ForceMode.Impulse);
        }
    }

    //Collision handling
    void OnCollisionEnter(Collision collision)
    {
        if (floorTags.Contains(collision.gameObject.tag))
        {
            if (!touchingFloor)
            {
                touchingFloor = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (floorTags.Contains(collision.gameObject.tag))
        {
            if (touchingFloor)
            {
                touchingFloor = false;
            }
        }
    }
    
    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        //react to button press
        if (Input.GetAxis("Vertical") != 0)
        {
            VerticalWalk(Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            HorizontalWalk(Input.GetAxis("Horizontal"));
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        

        //decelerate
        Decelerate();
    }

    #endregion
    //========================


}

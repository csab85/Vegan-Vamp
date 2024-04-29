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

    [SerializeField][Tooltip ("How much to ratio the horizontal max velocity ")] float horizontalMaxVRatio;

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
            print(rb.velocity);

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
            print(rb.velocity);

            Vector3 force = transform.right * direction * speed * Time.deltaTime;
            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    void Decelarate(float verticalInput, float horizontalInput)
    {
        if (verticalInput == 0)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(rb.velocity.x, rb.velocity.y, 0), 0.3f);
        }

        if (horizontalInput == 0)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, rb.velocity.y, rb.velocity.z), 0.3f);
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
        

        //decelerate
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            if (rb.velocity.x > 0 | rb.velocity.z > 0)
            {
                rb.drag = drag;
                print("aaaaaaaaaaaaaaaaaaa");
            }
        }

        else
        {
            rb.drag = 0;
        }
    }

    #endregion
    //========================


}

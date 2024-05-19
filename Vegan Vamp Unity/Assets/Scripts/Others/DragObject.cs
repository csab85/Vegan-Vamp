using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    Rigidbody2D rb;
    float baseGravity;

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

    public void Drag()
    {
        transform.position = Input.mousePosition;
        
        if (rb.simulated)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
    }

    public void Drop()
    {
        rb.gravityScale = baseGravity;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
    }

    #endregion
    //========================


}

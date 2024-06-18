using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DragObject : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    Inventory inventory;

    //components
    Rigidbody2D rb;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    bool dragging = false;
    bool hovering = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void Drag()
    {
        dragging = true;
    }

    public void BeginDrag()
    {
        rb.velocity = new Vector2(10, 10);
    }

    public void Drop()
    {
        dragging = false;
    }

    public void Hover()
    {
        hovering = true;
    }

    public void Unhover()
    {
        hovering = false;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        //get components
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dragging)
        {
            rb.MovePosition(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && hovering    )
        {
            inventory.DropItem(gameObject);
        }
    }

    #endregion
    //========================


}

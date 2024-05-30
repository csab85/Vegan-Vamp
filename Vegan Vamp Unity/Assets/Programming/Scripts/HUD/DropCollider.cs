using UnityEngine;

public class DropCollider : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Inventory inventory;
    [SerializeField] GameObject player;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        inventory.DropItem(collision.gameObject);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

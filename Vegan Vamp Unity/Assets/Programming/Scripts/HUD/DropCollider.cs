using System.Linq;
using UnityEngine;

public class DropCollider : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Inventory inventory;
    [SerializeField] GameObject player;
    [SerializeField] string[] targetTags;

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

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (targetTags.Contains(collider2D.tag))
        {
            inventory.DropItem(collider2D.gameObject);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

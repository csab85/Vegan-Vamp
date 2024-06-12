using System.Collections;
using UnityEngine;

public class InteriorCollider: MonoBehaviour
{
    //IMPORTS
    //========================
    #region



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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Juice")
        {
            collider.GetComponent<BottleIcon>().placeInBag = BottleIcon.Place.Inside;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Juice")
        {
            collider.GetComponent<BottleIcon>().placeInBag = BottleIcon.Place.Outside;
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

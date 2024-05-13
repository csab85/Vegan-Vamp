using UnityEngine;

public class GarlicClove : MonoBehaviour
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

    void OnCollisionEnter(Collision colllision)
    {
        StatsManager statsManager = colllision.gameObject.GetComponent<StatsManager>();

        if (statsManager != null)
        {
            //appliar o treco         
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

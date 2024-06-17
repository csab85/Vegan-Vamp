using UnityEngine;

public class SpeedIngredient : MonoBehaviour
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



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        if (transform.localScale.x <= 0.01f)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    //========================


}

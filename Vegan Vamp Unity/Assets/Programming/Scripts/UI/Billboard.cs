using UnityEngine;

public class Billlboard: MonoBehaviour
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
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    #endregion
    //========================


}

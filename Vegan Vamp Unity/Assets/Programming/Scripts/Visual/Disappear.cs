using UnityEngine;

public class Disappear : MonoBehaviour
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
        Vector3 scale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);

        transform.localScale = scale;

        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    //========================


}

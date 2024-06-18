using System.Collections;
using UnityEngine;

public class HealBurst : MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float duration;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(duration);

        if (duration > 0)
        {
            Destroy(gameObject);
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

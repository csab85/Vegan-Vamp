using System.Collections;
using UnityEngine;

public class Grow : MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float waitTime;
    bool growing = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator WaitToGrow()
    {
        yield return new WaitForSeconds(waitTime);

        growing = true;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        StartCoroutine(WaitToGrow());
    }

    void Update()
    {
        if (growing)
        {
            Vector3 scale = Vector3.MoveTowards(transform.localScale, new Vector3(8, 8, 8), Time.deltaTime * 3);

            transform.localScale = scale;

            if (transform.localScale.x >= 8)
            {
                growing = false;
            }
        }
    }

    #endregion
    //========================


}

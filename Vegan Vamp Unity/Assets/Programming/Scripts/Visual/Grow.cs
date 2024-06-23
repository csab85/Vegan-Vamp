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
            Vector3 scale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 0.5f);

            transform.localScale = scale;

            if (transform.localScale.x >= 1)
            {
                growing = false;
            }
        }
    }

    #endregion
    //========================


}

using System.Collections;
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

    [SerializeField] float waitTime;
    bool disappearing = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator WaitToDisappear()
    {
        yield return new WaitForSeconds(waitTime);

        disappearing = true;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        StartCoroutine(WaitToDisappear());
    }

    void Update()
    {
        if (disappearing)
        {
            Vector3 scale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);

            transform.localScale = scale;

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion
    //========================


}

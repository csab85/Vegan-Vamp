using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Transform tornadoCenter;
    [SerializeField] float pullForce;
    [SerializeField] float waitTime;

    StatsManager selfStats;

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

    IEnumerator pullObject(Collider objCollider, bool shouldPull)
    {
        if (shouldPull)
        {
            Vector3 forceDirection = tornadoCenter.position - objCollider.transform.position;
            float actualPullForce = pullForce * transform.localScale.x;

            objCollider.GetComponent<Rigidbody>().AddForce(forceDirection * actualPullForce * Time.deltaTime);
            
            yield return new WaitForSeconds(waitTime);
            
            StartCoroutine(pullObject(objCollider, true));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(pullObject(collider, true));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(pullObject(collider, false));
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        selfStats = GetComponent<StatsManager>();
    }

    void Update()
    {
        if (selfStats.tornado[StatsConst.SELF_INTENSITY] > 0)
        {
            float selfScale = selfStats.tornado[StatsConst.SELF_INTENSITY];

            transform.localScale = new Vector3(selfScale, selfScale, selfScale);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    //========================


}

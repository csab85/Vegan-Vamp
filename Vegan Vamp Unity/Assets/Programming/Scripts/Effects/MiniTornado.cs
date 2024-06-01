using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class MiniTornado : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    TornadoFruit tornadoFruit;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] Transform tornadoCenter;
    [SerializeField] float pullForce;
    [SerializeField] float waitTime;
    [SerializeField] float widingSpeed;
    [SerializeField] float maxWideness;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator pullObject(Collider objCollider, bool shouldPull)
    {
        if (shouldPull)
        {
            //pull objects with rigid body
            Vector3 forceDirection = tornadoCenter.position - objCollider.transform.position;
            float actualPullForce = pullForce * transform.localScale.x;

            objCollider.GetComponent<Rigidbody>().AddForce(forceDirection * actualPullForce * Time.deltaTime);

            //wait and restart coroutine
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
        tornadoFruit = transform.parent.GetComponent<TornadoFruit>();
    }

    void Update()
    {
        float newScale = transform.localScale.x + widingSpeed * Time.deltaTime;
        
        transform.localScale = new Vector3(newScale, transform.localScale.y, newScale);

        if (newScale  > maxWideness)
        {
            transform.localScale = new Vector3(0.01f, transform.localScale.y, 0.01f);
            tornadoFruit.itsTornadoTime = true;
            gameObject.SetActive(false);
        }
    }

    #endregion
    //========================


}

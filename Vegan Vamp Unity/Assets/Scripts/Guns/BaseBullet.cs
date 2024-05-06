using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class BaseBullet : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] GameObject parent;
    Rigidbody rb;

    [Header ("Scripts")]
    [SerializeField] Gun gunScript;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float maxDistance;
    [SerializeField] bool returnOnCollision;
    
    [Header ("Info")]
    [SerializeField] public bool movingToTarget;
    
    //aim
    Vector3 mouseWorldPosition;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void ReturnToPool()
    {
        rb.velocity = Vector3.zero;
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        movingToTarget = false;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        movingToTarget = false;

        if (returnOnCollision)
        {
            ReturnToPool();
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void OnEnable()
    {   
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        Vector3 aimDirection = gunScript.aimHit.point - transform.position;

        rb.AddForce(aimDirection, ForceMode.Impulse);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, parent.transform.position) > maxDistance)
        {
            ReturnToPool();
        }
    }

    #endregion
    //========================


}

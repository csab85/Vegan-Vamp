using Unity.VisualScripting;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] GameObject parent;
    [SerializeField] MainCamera mainCameraScript;
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
    [SerializeField] bool targeted;
    [SerializeField] public bool movingToTarget;
    
    

    Vector3 target;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void ReturnToPool()
    {
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        targeted = false;
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

    void Update()
    {
        if (Vector3.Distance(transform.position, parent.transform.position) > maxDistance)
        {
            ReturnToPool();
        }

        if (!targeted)
        {
            target = mainCameraScript.aimHit.point;
            targeted = true;
        }

        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, gunScript.shotPower * Time.deltaTime);
        }
    }

    #endregion
    //========================


}

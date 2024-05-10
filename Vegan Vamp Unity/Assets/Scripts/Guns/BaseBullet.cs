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

    GameObject hit;

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
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (returnOnCollision)
        {
            ReturnToPool();
        }

        // GameObject hit = hitPool.transform.GetChild(0).gameObject;
        // VisualEffect hitFx = hit.GetComponent<VisualEffect>();

        // hit.transform.SetParent(null);
        // hit.transform.position = aimHit.point;
        // hit.SetActive(true);
        // hitFx.Play();
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {   
        rb.velocity = Vector3.zero;

        Vector3 aimDirection = gunScript.aimHit.point - transform.position;

        rb.AddForce(aimDirection.normalized * gunScript.shotPower, ForceMode.Impulse);
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

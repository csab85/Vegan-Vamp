using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.VFX;

public class BaseBullet : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] GameObject parent;
    [SerializeField] GameObject hitPool;

    [Header ("Scripts")]
    [SerializeField] Gun gunScript;

    Rigidbody rb;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float maxDistance;
    [SerializeField] float fxDelay;
    [SerializeField] bool returnOnCollision;

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

    IEnumerator PlayHitFx()
    {
        GameObject hit = hitPool.transform.GetChild(0).gameObject;
        VisualEffect hitFx = hit.GetComponent<VisualEffect>();

        hit.transform.SetParent(null);
        hit.transform.position = transform.position;

        yield return new WaitForSecondsRealtime(fxDelay);

        hit.SetActive(true);
        hitFx.Play();

        if (returnOnCollision)
        {
            ReturnToPool();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(PlayHitFx());
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

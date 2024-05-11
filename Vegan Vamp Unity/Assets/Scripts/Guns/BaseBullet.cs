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

    GameObject parent;
    Gun gunScript;
    Rigidbody rb;
    MeshRenderer meshRenderer;
    GameObject hit;
    VisualEffect hitFx;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float maxDistance;
    [SerializeField] bool returnOnCollision;
    [SerializeField] float returnDelay;
    [SerializeField] bool playFxOnCollision;
    [SerializeField] float fxDelay;
    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    /// <summary>
    /// Waits for the delay and resets the bullet to its initial state (including posit), then deactivates it
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(returnDelay);

        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        meshRenderer.enabled = true;
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(90, 0, 0);

        hit.SetActive(false);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Waits for the delay then plays the hit effect
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayHitFx() 
    {
        yield return new WaitForSecondsRealtime(fxDelay);

        hit.SetActive(true);
        meshRenderer.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;

        if (playFxOnCollision)
        {
            StartCoroutine(PlayHitFx());
        }

        if (returnOnCollision)
        {
            StartCoroutine(ReturnToPool());
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Awake()
    {
        parent = transform.parent.gameObject;
        gunScript = parent.transform.parent.GetComponent<Gun>();
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        hit = transform.GetChild(0).gameObject;
        hitFx = hit.GetComponent<VisualEffect>();
    }

    void OnEnable()
    {   
        Vector3 aimDirection = gunScript.aimHit.point - transform.position;

        rb.AddForce(aimDirection.normalized * gunScript.shotPower, ForceMode.Impulse);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, parent.transform.position) > maxDistance)
        {
            StartCoroutine(ReturnToPool());
        }
    }

    #endregion
    //========================


}

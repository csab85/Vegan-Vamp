using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CornGrenade : MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    
    Gun gunScript;
    BaseBullet bulletScript;
    Rigidbody rb;


    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField][Tooltip ("Seconds until automatic explosions")] float explosionCountdown;
    [SerializeField] float explosionSize;
    [SerializeField] float explosionPower;

    bool exploding = false;
    bool exploded = false;
    MeshRenderer meshRenderer;
    int fragNumber;
    public int fragCount = 0;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    /// <summary>
    /// Applies explosion force on the given targets, if they have rigid body, then returns to pool
    /// </summary>
    /// <param name="targets">Objects to apply force on</param>
    void Explode(Collider[] targets)
    {
        //add force to each target
        foreach (Collider target in targets)
        {
            Rigidbody targetRb = target.gameObject.GetComponent<Rigidbody>();

            if(targetRb != null)
            {
                targetRb.AddExplosionForce(explosionPower, transform.position, explosionSize);
            }
        }

        //play explosion fx
        StartCoroutine(bulletScript.PlayHitFx());

        //turn off renderer
        meshRenderer.enabled = false;

        //make popcorns pop
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Projectile")
            {
                child.gameObject.SetActive(true);
            }
        }

        //start checking if all of them are disabled
        exploded = true;
    }

    /// <summary>
    /// Waits for the countdown time, then gets every collider within an area. Then uses the explosion function on them
    /// </summary>
    /// <returns></returns>
    IEnumerator ExplosionCountdown()
    {
        Collider[] targets = {};

        yield return new WaitForSecondsRealtime(explosionCountdown);

        targets = Physics.OverlapSphere(transform.position, explosionSize);

        Explode(targets);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!exploding)
        {
            exploding = true;
            rb.isKinematic = true;
            StartCoroutine(ExplosionCountdown());
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void OnEnable()
    {
        foreach(Transform child in transform)
        {
            child.transform.localPosition = Vector3.zero;
        }
    }

    void Awake()
    {   
        gunScript = transform.parent.GetComponent<Gun>();
        bulletScript = GetComponent<BaseBullet>();
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        fragNumber = transform.childCount - 1;
    }

    void Update()
    {
        if (exploded)
        {
            if (fragCount == fragNumber)
            {
                exploded = false;
                exploding = false;
                fragCount = 0;
                StartCoroutine(bulletScript.ReturnToPool());
            }
        }
    }

    #endregion
    //========================


}

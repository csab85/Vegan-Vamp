using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PopcornFrag : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] [Range(0,  10)] float maxForce;
    [SerializeField] [Range(0, 10)] float minForce;
    [SerializeField] [Range(0, 10)] float minCountdown;
    [SerializeField] [Range(0, 10)] float maxCountdown;
    [SerializeField] float explosionSize;
    [SerializeField] float explosionPower;

    GameObject parent;
    CornGrenade grenadeScript;
    Rigidbody rb;
    GameObject explosion;
    VisualEffect explosionFx;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    Vector3 randomDirection;
    float randomForce;
    float randomCountdown;
    Collider[] targets;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator Explode()
    {
        //wait to explode
        yield return new WaitForSeconds(randomCountdown);

        //get targets using physics
        targets = Physics.OverlapSphere(transform.position, explosionSize);

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
        explosion.SetActive(true);
        explosionFx.Play();

        yield return new WaitForSeconds(0.1f);

        //update counting on the grenade script (so it knows when to reset)
        grenadeScript.fragCount ++;
        explosion.SetActive(false);
        gameObject.SetActive(false);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Awake()
    {
        parent = transform.parent.gameObject;
        grenadeScript = parent.GetComponent<CornGrenade>();
        rb = GetComponent<Rigidbody>();
        explosion = transform.GetChild(0).gameObject;
        explosionFx = explosion.GetComponent<VisualEffect>();
    }

    void OnEnable()
    {   
        //calculate random values
        randomDirection = Random.onUnitSphere;
        randomForce = Random.Range(minForce, maxForce);
        randomCountdown = Random.Range(minCountdown, maxCountdown);

        randomDirection.y  = Mathf.Abs(randomDirection.y);

        rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        StartCoroutine(Explode());
    }

    #endregion
    //========================


}

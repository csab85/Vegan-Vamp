using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CornGrenade : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Self Imports")]
    [SerializeField] BaseBullet bulletScript;
    [SerializeField] Rigidbody rb;


    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField][Tooltip ("Seconds until automatic explosions")] float explosionCountdown;
    [SerializeField] float explosionSize;
    [SerializeField] float explosionPower;

    [Header ("Info")]
    [SerializeField] bool contando;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Explode(Collider[] targets)
    {

        foreach (Collider target in targets)
        {
            Rigidbody targetRb = target.gameObject.GetComponent<Rigidbody>();

            if(targetRb != null)
            {
                targetRb.AddExplosionForce(explosionPower, transform.position, explosionSize);
                print("BUUUUUM");
                rb.isKinematic = false;
                bulletScript.ReturnToPool();
            }
        }
    }

    IEnumerator ExplosionCountdown()
    {
        contando = true;

        Collider[] targets = {};

        yield return new WaitForSecondsRealtime(explosionCountdown);

        targets = Physics.OverlapSphere(transform.position, explosionSize);

        Explode(targets);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!contando)
        {

            print("contando");
            rb.isKinematic = true;
            StartCoroutine(ExplosionCountdown());
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        bulletScript = GetComponent<BaseBullet>();
        rb = GetComponent<Rigidbody>();
    }

    #endregion
    //========================


}

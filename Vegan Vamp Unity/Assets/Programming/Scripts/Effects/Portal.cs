using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Portal : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //Game objects
    GameObject player;
    [SerializeField] GameObject otherPortal;

    //components
    SphereCollider sphereCollider;
    VisualEffect otherPortalParticles;


    //scripts
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] int portalNumber;

    bool puffing;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name != "Base Juice")
        {
            otherPortalParticles.Play();
            collider.transform.position = otherPortal.transform.position + (otherPortal.transform.forward * 2);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get player
        player  = GameObject.Find("Player");

        //get components
        sphereCollider = GetComponent<SphereCollider>();
        otherPortalParticles = otherPortal.transform.Find("Particles").gameObject.GetComponent<VisualEffect>();

        //get scripts
        selfStats = GetComponent<StatsManager>();

        //get stats from parent
        GameObject parent = transform.parent.gameObject;
        StatsManager parentStats = parent.GetComponent<StatsManager>();

        selfStats = parentStats;

        //set null parent
        transform.parent = null;

        //set positions
        if (portalNumber == 1)
        {
            transform.position = player.transform.position + (player.transform.forward * 3);
            transform.position += new Vector3(0, 0.9f, 0);
        }

        else
        {
            transform.position += new Vector3(0, 1.5f, 0);
        }

        sphereCollider.enabled = true;
    }

    void Update()
    {
        float selfScale = selfStats.teleport[StatsConst.SELF_INTENSITY];
        transform.localScale = new Vector3(selfScale, selfScale, selfScale);
    }

    #endregion
    //========================


}

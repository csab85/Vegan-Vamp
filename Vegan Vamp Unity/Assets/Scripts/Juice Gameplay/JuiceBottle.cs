using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;

public class JuiceBottle : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] GameObject splash;
    [SerializeField] Rigidbody rb;

    BoxCollider bc;
    Ray aimRay;
    RaycastHit aimHit;

    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float throwPower;
    [SerializeField] public float splashRange;
    [SerializeField] LayerMask targetLayers;

    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    private void Break()
    {
        Intact.SetActive(false);
        Broken.SetActive(true);

        splash.transform.parent = null;
        splash.SetActive(true);

        bc.enabled = false;

        //apply effects on targets
        Collider[] targets;

        targets = Physics.OverlapSphere(transform.position, splashRange, targetLayers);

        foreach (Collider targetCollider in targets)
        {
            GameObject target = targetCollider.gameObject;

            //apply every stat on the object (if the stat has any spply intensity)
            foreach (var item in selfStats.statsDict)
            {
                StatsManager.Stats stat = item.Key;
                float applyIntensity = selfStats.statsDict[stat][APPLY_INTENSITY];
                float applyDuration = selfStats.statsDict[stat][APPLY_DURATION];

                if (applyIntensity != 0)
                {
                    target.GetComponent<StatsManager>().ApplyStatSelf(stat, applyIntensity, applyDuration);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Break();
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Awake()
    {
        Intact.SetActive(true);
        Broken.SetActive(false);

        bc = GetComponent<BoxCollider>();

        selfStats = GetComponent<StatsManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Throw"))
        {
            transform.parent = null;

            Vector3 aimDirection = aimHit.point - transform.position;

            rb.isKinematic = false;
            rb.AddForce(aimDirection.normalized * throwPower, ForceMode.Impulse);
        }

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
    }

    #endregion
    //========================


}

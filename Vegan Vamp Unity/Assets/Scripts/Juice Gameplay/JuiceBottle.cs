using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;

public class JuiceBottle : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] public GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] GameObject splash;
    [SerializeField] Rigidbody rb;

    BoxCollider bc;
    Ray aimRay;
    RaycastHit aimHit;

    StatsManager selfStats;
    bool smashable = false;

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

    void Break()
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
            print(targetCollider.gameObject.name);
            GameObject target = targetCollider.gameObject;

            //apply every stat on the object (if the stat has any spply intensity)
            for (int i = 0; i < selfStats.statsArray.Count(); i++)
            {
                float applyIntensity = selfStats.statsArray[i][APPLY_INTENSITY];
                float applyDuration = selfStats.statsArray[i][APPLY_DURATION];

                if (applyIntensity != 0)
                {
                    //se pa erro aqui FIX THIS
                    //target.GetComponent<StatsManager>().ApplyStatSelf(i, applyIntensity, applyDuration);
                }
            }
        }
    }

    //ISSO AQUI É PROVISORIO
    public void GrabJuice(GameObject juice)
    {   
        //copy stats
        for (int i = 0; i < selfStats.statsArray.Count(); i++)
        {
            for(int j = 0; j < 9; j++)
            {
                selfStats.statsArray[i][j] = juice.GetComponent<StatsManager>().statsArray[i][j];
            }
        }

        Destroy(juice);

        //make it visible if not
        if (!Intact.activeSelf)
        {
            Intact.SetActive(true);
            Broken.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (smashable)
        {
            Break();
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Awake()
    {
        // Intact.SetActive(true);
        // Broken.SetActive(false);

        bc = GetComponent<BoxCollider>();

        selfStats = GetComponent<StatsManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Throw") && Intact.activeSelf && gameObject.name == "Base Juice")
        {   
            Vector3 spawnPoint = transform.position + transform.forward * 0.7f;
            GameObject copyJuice = Instantiate(gameObject, spawnPoint, Quaternion.identity, null);

            Vector3 aimDirection = aimHit.point - transform.position;

            copyJuice.GetComponent<JuiceBottle>().smashable = true;
            copyJuice.GetComponent<Rigidbody>().isKinematic = false;
            copyJuice.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * throwPower, ForceMode.Impulse);

            Intact.SetActive(false);
        }

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
    }

    #endregion
    //========================


}

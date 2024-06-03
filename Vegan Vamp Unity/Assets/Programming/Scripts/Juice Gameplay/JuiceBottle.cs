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
    [SerializeField] GameObject player;
    [SerializeField] public GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] GameObject splash;
    [SerializeField] Rigidbody rb;

    GameObject tornado;

    Animator animator;
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
                float applyIntensity = selfStats.statsArray[i][StatsConst.APPLY_INTENSITY];
                float applyReachTime = selfStats.statsArray[i][StatsConst.APPLY_REACH_TIME];
                float applyReturnTime = selfStats.statsArray[i][StatsConst.APPLY_RETURN_TIME];

                if (applyIntensity != 0)
                {
                    target.GetComponent<StatsManager>().ApplyStatSelf(i, applyIntensity, applyReachTime, applyReturnTime);
                }
            }
        }

        //Juice effects that don't need a target

        //activate tornado
        if (selfStats.tornado[StatsConst.APPLY_INTENSITY] > 0)
        {
            tornado.transform.parent = null;
            tornado.transform.rotation = Quaternion.identity;
            tornado.SetActive(true);

            //apply tornado ice and fire
            tornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.TORNADO, selfStats.tornado[StatsConst.APPLY_INTENSITY], selfStats.tornado[StatsConst.APPLY_REACH_TIME], selfStats.tornado[StatsConst.APPLY_RETURN_TIME]);

            tornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.ICE, selfStats.ice[StatsConst.APPLY_INTENSITY], selfStats.ice[StatsConst.APPLY_REACH_TIME], selfStats.ice[StatsConst.APPLY_RETURN_TIME]);

            tornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.FIRE, selfStats.fire[StatsConst.APPLY_INTENSITY], selfStats.fire[StatsConst.APPLY_REACH_TIME], selfStats.fire[StatsConst.APPLY_RETURN_TIME]);
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

            //update hold animation
            animator.SetLayerWeight(AnimationConsts.BOTTLE_LAYER, 1);
        }
    }

    public void ThrowBottle()
    {
        //make not throwable if you're dead
        if (Intact.activeSelf && gameObject.name == "Base Juice")
        {   
            Vector3 spawnPoint = transform.position + transform.forward * 0.4f;
            GameObject copyJuice = Instantiate(gameObject, spawnPoint, gameObject.transform.rotation, null);

            Vector3 aimDirection = aimHit.point - transform.position;

            copyJuice.transform.localScale = Vector3.one;
            copyJuice.GetComponent<JuiceBottle>().smashable = true;
            copyJuice.GetComponent<Rigidbody>().isKinematic = false;
            copyJuice.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * throwPower, ForceMode.Impulse);

            Intact.SetActive(false);

            //deactivate animation layer
            animator.SetLayerWeight(AnimationConsts.BOTTLE_LAYER, 0);
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

        animator = player.GetComponent<Animator>();

        tornado = transform.Find("VFX Tornado").gameObject;

        bc = GetComponent<BoxCollider>();

        selfStats = GetComponent<StatsManager>();
    }

    void Update()
    {
        //throw bottle
        if (Input.GetButtonDown("Throw"))
        {
            animator.SetTrigger("Throw");
        }

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
    }

    #endregion
    //========================


}

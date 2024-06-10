using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;

public class JuiceBottle : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    //game objects
    [SerializeField] GameObject player;
    [SerializeField] public GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] GameObject splash;
    [SerializeField] GameObject tornado;
    [SerializeField] GameObject portal;

    //components
    Animator animator;
    BoxCollider bc;

    //scripts
    StatsManager selfStats;

    //extras
    Ray aimRay;
    RaycastHit aimHit;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float throwPower;
    [SerializeField] public float splashRange;
    [SerializeField] LayerMask targetLayers;

    bool smashable = false;

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
            GameObject target = targetCollider.gameObject;
            print(targetCollider.name);

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

        //spawn tornado
        if (selfStats.tornado[StatsConst.APPLY_INTENSITY] > 0)
        {
            GameObject newTornado = Instantiate(tornado, transform.position, Quaternion.identity, null);
            newTornado.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            //apply tornado ice and fire
            newTornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.TORNADO, selfStats.tornado[StatsConst.APPLY_INTENSITY], selfStats.tornado[StatsConst.APPLY_REACH_TIME], selfStats.tornado[StatsConst.APPLY_RETURN_TIME]);

            newTornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.ICE, selfStats.ice[StatsConst.APPLY_INTENSITY], selfStats.ice[StatsConst.APPLY_REACH_TIME], selfStats.ice[StatsConst.APPLY_RETURN_TIME]);

            newTornado.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.FIRE, selfStats.fire[StatsConst.APPLY_INTENSITY], selfStats.fire[StatsConst.APPLY_REACH_TIME], selfStats.fire[StatsConst.APPLY_RETURN_TIME]);
        }

        //spawn teleport
        if (selfStats.teleport[StatsConst.APPLY_INTENSITY] > 0)
        {
            GameObject newPortal = Instantiate(portal, transform.position, Quaternion.identity, null);
            newPortal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            //apply teleport
            newPortal.GetComponent<StatsManager>().ApplyStatSelf(StatsConst.TELEPORT, selfStats.teleport[StatsConst.APPLY_INTENSITY], selfStats.teleport[StatsConst.APPLY_REACH_TIME], selfStats.teleport[StatsConst.APPLY_RETURN_TIME]);
        }
    }

    //ISSO AQUI É PROVISORIO
    public void GrabJuice(GameObject juice)
    {   
        //copy stats
        selfStats.CopyStats(juice.GetComponent<StatsManager>());

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
            Vector3 spawnPoint = transform.position + Camera.main.transform.forward * 0.2f;
            GameObject copyJuice = Instantiate(gameObject, spawnPoint, gameObject.transform.rotation, null);

            Vector3 aimDirection = aimHit.point - transform.position;

            copyJuice.transform.localScale = Vector3.one;
            copyJuice.GetComponent<BoxCollider>().isTrigger = false;
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

        //get components
        animator = player.GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();

        //get scripts
        selfStats = GetComponent<StatsManager>();
    }

    void FixedUpdate()
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

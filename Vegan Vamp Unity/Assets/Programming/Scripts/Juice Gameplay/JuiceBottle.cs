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

    //game objects
    [Header ("Game Objects")]
    GameObject player;
    [SerializeField] public GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] GameObject splash;
    [SerializeField] GameObject tornado;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject heal;

    //components
    Animator animator;
    BoxCollider bc;

    //scripts
    [Header ("Scripts")]
    [SerializeField] Inventory inventory;
    [SerializeField] Tutorial tutorial;
    Hotbar hotbar;
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
    Transform baseTransform;

    bool smashable = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void ActivateBottle()
    {
        Intact.SetActive(true);
        Broken.SetActive(false);

        //update hold animation
        animator.SetLayerWeight(AnimationConsts.BOTTLE_LAYER, 1);
    }

    public void DeactivateBottle()
    {
        Intact.SetActive(false);
        Broken.SetActive(false);
    }

    //ISSO AQUI É PROVISORIO
    public void GrabJuice(GameObject juice)
    {   
        //Add to bag
        inventory.AddItem(juice);

        Destroy(juice);
    }

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

        if (targets.Length > 0)
        {
            foreach (Collider targetCollider in targets)
            {
                GameObject target = targetCollider.gameObject;
                print(targetCollider.name);

                //apply every stat on the object (if the stat has any spply intensity)
                for (int i = 0; i < selfStats.statsArray.Count(); i++)
                {
                    //run normally if it isnt health effect
                    if (i != StatsConst.HEALTH)
                    {
                        float applyIntensity = selfStats.statsArray[i][StatsConst.APPLY_INTENSITY];
                        float applyReachTime = selfStats.statsArray[i][StatsConst.APPLY_REACH_TIME];
                        float applyReturnTime = selfStats.statsArray[i][StatsConst.APPLY_RETURN_TIME];
                        
                        if (applyIntensity != 0)
                        {
                            target.GetComponent<StatsManager>().ApplyStatSelf(i, applyIntensity, applyReachTime, applyReturnTime);
                        }
                    }

                    //apply to base if health
                    else
                    {
                        float applyIntensity = selfStats.statsArray[i][StatsConst.APPLY_INTENSITY];
                        float applyReturnTime = selfStats.statsArray[i][StatsConst.APPLY_RETURN_TIME];

                        target.GetComponent<StatsManager>().ApplyToBase(i, applyIntensity);
                    }
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

        //spawn heal
        if (selfStats.health[StatsConst.APPLY_INTENSITY] > 0)
        {
            float healScale = selfStats.health[StatsConst.APPLY_INTENSITY];

            GameObject newHeal = Instantiate(heal, transform.position, Quaternion.identity, null);
            newHeal.transform.localScale = new Vector3(healScale, healScale, healScale);
        }

        //spawn explosion
        //cody things here

        //tutorial
        tutorial.tutorialSteps = 3;
    }

    public void ThrowBottle()
    {
        //make not throwable if you're dead
        if (Intact.activeSelf && gameObject.name == "Base Juice")
        {   
            //create and throw juice
            Vector3 spawnPoint = transform.position + Camera.main.transform.forward * 0.2f;
            GameObject copyJuice = Instantiate(gameObject, spawnPoint, gameObject.transform.rotation, null);

            Vector3 aimDirection = aimHit.point - transform.position;

            copyJuice.transform.localScale = Vector3.one;
            copyJuice.GetComponent<BoxCollider>().isTrigger = false;
            copyJuice.GetComponent<JuiceBottle>().smashable = true;
            copyJuice.GetComponent<Rigidbody>().isKinematic = false;
            copyJuice.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * throwPower, ForceMode.Impulse);

            Intact.SetActive(false);

            //delete juice on hotbar
            hotbar.SeekAndDestroyBottles();

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
        //get game object
        player = GameObject.Find("Player").gameObject;

        //get components
        animator = player.GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();

        //get scripts
        hotbar = GameObject.Find("Hotbar").GetComponent<Hotbar>();
        selfStats = GetComponent<StatsManager>();

        //get values
        baseTransform = transform;
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

        if (baseTransform.position != transform.position)
        {
            transform.position = baseTransform.position;
            transform.rotation = baseTransform.rotation;
        }
    }

    #endregion
    //========================


}

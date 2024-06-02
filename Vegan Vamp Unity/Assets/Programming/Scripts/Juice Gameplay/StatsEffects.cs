using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//VFX container gotta be the third object

public class StatsEffects : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    StatsManager selfStats;

    //VFX
    GameObject fire;

    //models
    List<GameObject> iceCubesList = new List<GameObject>();

    Animator animator;
    NavMeshAgent agent;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    //Fire Damage
    [Tooltip ("Can burn but won't take damage")]
    [SerializeField] bool fireProof;
    static float fireDamage = 0.1f;
    static float fireRefreshRate = 0.05f;


    //Check what coroutine is running
    bool burning;
    bool frozen;

    float baseSpeed;

    //extras
    int iceNumber;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator FireDOT()
    {
        if (burning)
        {
            selfStats.health[StatsConst.DEFAULT_BASE] -= fireDamage;
            yield return new WaitForSeconds(fireRefreshRate);
            StartCoroutine(FireDOT());
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        selfStats = GetComponent<StatsManager>();
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<Animator>(out animator);

        //Get vfx objects
        Transform VFX = transform.Find("VFX");

        if (selfStats.fire[StatsConst.CAP_INTENSITY] != 0 && selfStats.objectType != StatsManager.Type.None)
        {
            fire = VFX.GetChild(0).gameObject;
        }

        //get models
        if (selfStats.ice[StatsConst.CAP_INTENSITY] != 0 && selfStats.objectType != StatsManager.Type.None)
        {
            foreach (Transform iceCube in transform.Find("Models").Find("Ice Cubes"))
            {
                iceCubesList.Add(iceCube.gameObject);
            }
        }
    }

    void Update()
    {
        //EFFECTS
        if (selfStats.objectType != StatsManager.Type.None)
        {
            //HEALTH
            if (selfStats.dead)
            {
                if (selfStats.objectType == StatsManager.Type.Ingredient)
                {
                    Destroy(gameObject);
                }

                if (selfStats.objectType == StatsManager.Type.Other)
                {
                    Destroy(gameObject);
                }

                if (selfStats.objectType == StatsManager.Type.NPC)
                {
                    Destroy(gameObject);
                }

                if (selfStats.objectType == StatsManager.Type.Player)
                {
                    animator.SetLayerWeight(AnimationConsts.DAMAGE_LAYER, 1);
                    animator.Play("Death", AnimationConsts.DAMAGE_LAYER);
                    gameObject.GetComponent<CapsuleCollider>().height = 0.1f;
                }
            }

            //FIRE
            if (fire != null)
            {
                if (selfStats.fire[StatsConst.SELF_INTENSITY] > 0)
                {
                    if (!burning)
                    {
                        burning = true;
                        //damage over time
                        if (!fireProof)
                        {
                            StartCoroutine(FireDOT());
                        }
                    }

                    //activate fire fx
                    if (!fire.activeSelf)
                    {
                        fire.SetActive(true);
                    }

                    float fireScale = selfStats.fire[StatsConst.SELF_INTENSITY];

                    fire.transform.localScale = new Vector3(fireScale, fireScale, fireScale);

                    //make unpickable if ingredient
                    if (selfStats.objectType == StatsManager.Type.Ingredient)
                    {
                        gameObject.layer = LayerMask.NameToLayer("Ingredient Part");
                    }
                }

                else
                {
                    if (burning)
                    {
                        burning = false;
                    }

                    if (fire.activeSelf)
                    {
                        fire.SetActive(false);
                    }

                    //make pickable if ingredient
                    if (selfStats.objectType == StatsManager.Type.Ingredient)
                    {
                        gameObject.layer = LayerMask.NameToLayer("Ingredient");
                    }
                }
            }
            

            //ICE
            if (iceCubesList.Count > 0)
            {
                if (selfStats.ice[StatsConst.SELF_INTENSITY] > 0)
                {
                    if (!frozen)
                    {
                        iceNumber = Random.Range(0, iceCubesList.Count);
                    }

                    GameObject iceCube = iceCubesList[iceNumber];

                    if (!iceCube.activeSelf)
                    {
                        iceCube.SetActive(true);
                    }

                    float iceScale = selfStats.ice[StatsConst.SELF_INTENSITY];

                    iceCube.transform.localScale = new Vector3(iceScale, iceScale, iceScale);

                    if (animator != null && agent != null)
                    {
                        animator.speed = 0;
                        agent.speed = 0;
                    }

                    frozen = true;

                    //make unpickable if ingredient
                    if (selfStats.objectType == StatsManager.Type.Ingredient)
                    {
                        gameObject.layer = LayerMask.NameToLayer("Ingredient Part");
                    }
                }

                else if (frozen)
                {
                    frozen = false;

                    foreach (GameObject iceCube in iceCubesList)
                    {
                        iceCube.SetActive(false);
                    }

                    if (animator != null && agent != null)
                    {
                        animator.speed = 1;
                        agent.speed = baseSpeed;
                    }

                    //make pickable if ingredient
                    if (selfStats.objectType == StatsManager.Type.Ingredient)
                    {
                        gameObject.layer = LayerMask.NameToLayer("Ingredient");
                    }
                }
        }
        }
    }

    #endregion
    //========================


}

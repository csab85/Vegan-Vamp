using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

//VFX container gotta be the third object

public class StatsEffects : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    StatsManager selfStats;

    //VFX
    GameObject fire;
    GameObject gravityParticles;
    MeshTrail meshTrail;

    //models
    List<GameObject> iceCubesList = new List<GameObject>();

    //components
    Rigidbody rb;
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
            selfStats.ApplyToBase(StatsConst.HEALTH, -fireDamage);
            yield return new WaitForSeconds(fireRefreshRate);
            StartCoroutine(FireDOT());
        }
    }

    public void DamageSelf(Vector3 knockbackDir, float dmg)
    {
        if (!selfStats.dead)
        {
            rb.AddForce(knockbackDir * 5, ForceMode.Impulse);
            selfStats.ApplyToBase(StatsConst.HEALTH, -dmg);

            if (gameObject.tag == "Player")
            {
                animator.SetLayerWeight(AnimationConsts.DAMAGE_LAYER, 1);
            }

            animator.Play("Damage");   
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
        TryGetComponent<MeshTrail>(out meshTrail);
        TryGetComponent<Rigidbody>(out rb);

        //Get vfx objects
        Transform VFX = transform.Find("VFX");

        if (selfStats.fire[StatsConst.CAP_INTENSITY] != 0 && selfStats.objectType != StatsManager.Type.None)
        {
            fire = VFX.transform.Find("VFX Fire").gameObject;
        }

        if (selfStats.noGravity[StatsConst.CAP_INTENSITY] != 0 && selfStats.objectType != StatsManager.Type.None)
        {
            gravityParticles = VFX.transform.Find("VFX No Gravity").gameObject;
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
                    GetComponent<FieldOfView>().enabled = false;

                    animator.Play("Death");
                }

                if (selfStats.objectType == StatsManager.Type.Player)
                {
                    animator.SetLayerWeight(AnimationConsts.DAMAGE_LAYER, 1);
                    animator.Play("Death", AnimationConsts.DAMAGE_LAYER);
                }
            }

            //FIRE
            if (fire)
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


            //SPEED
            if (meshTrail)
            {
                if (selfStats.speed[StatsConst.SELF_INTENSITY] > 0)
                {
                    //update speed multiplier on stats manager
                    float bonusSpeed = selfStats.speed[StatsConst.SELF_INTENSITY] + 1;

                    if (selfStats.speedMultiplier != bonusSpeed)
                    {
                        selfStats.speedMultiplier = bonusSpeed;
                    }

                    //enable mesh trail
                    if (!meshTrail.isTrailActive)
                    {
                        StartCoroutine(meshTrail.ActivateTrail());
                    }

                    //set trail rate
                    meshTrail.meshRefreshRate = 0.1f / (selfStats.speed[StatsConst.SELF_INTENSITY] + 1);
                }

                else if(selfStats.speed[StatsConst.SELF_INTENSITY] <= 0)
                {
                    if (selfStats.speedMultiplier != 1)
                    {
                        selfStats.speedMultiplier = 1;
                    }

                    if (meshTrail.isTrailActive)
                    {
                        meshTrail.isTrailActive = false;
                    }
                }
            }

            //NO GRAVITY
            if (gravityParticles)
            {
                if (selfStats.noGravity[StatsConst.SELF_INTENSITY] > 0)
                {
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                    rb.mass = 1 / (selfStats.noGravity[StatsConst.SELF_INTENSITY] + 1);

                    if (!gravityParticles.activeSelf)
                    {
                        gravityParticles.SetActive(true);
                    }

                    //set particles spawn

                    //to make particle not disappear suddenly
                    float multiplier = Mathf.Clamp(selfStats.noGravity[StatsConst.SELF_INTENSITY], 0, 1);


                    int spawnRate = Shader.PropertyToID("Spawn Rate");
                    gravityParticles.GetComponent<VisualEffect>().SetFloat(spawnRate, selfStats.noGravity[StatsConst.SELF_INTENSITY] * 100 * multiplier);
                }

                else
                {
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                    if (rb.mass != 1)
                    {
                        rb.mass = 1;
                    }

                    if (gravityParticles.activeSelf)
                    {
                        gravityParticles.SetActive(false);
                    }
                }
            }

        }
    }

    #endregion
    //========================


}

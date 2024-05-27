using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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



    //Consts
    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

    //Stats order
    const int HEALTH = 0;
    const int FIRE = 1;
    const int ICE = 0;

    //type of object
    enum Type
    {
        Entity,
        Ingredient
    }

    [SerializeField] Type objectType;

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

        if (selfStats.fire[CAP_INTENSITY] != 0)
        {
            fire = VFX.GetChild(0).gameObject;
        }

        //get models
        if (selfStats.ice[CAP_INTENSITY] != 0)
        {
            foreach (Transform iceCube in transform.Find("Models").Find("Ice Cubes"))
            {
                iceCubesList.Add(iceCube.gameObject);
            }
        }
    }

    void Update()
    {
        //FIRE
        if (fire != null)
        {
            if (selfStats.fire[SELF_INTENSITY] > 0)
            {
                if (!fire.activeSelf)
                {
                    fire.SetActive(true);
                }

                float fireScale = selfStats.fire[SELF_INTENSITY];

                fire.transform.localScale = new Vector3(fireScale, fireScale, fireScale);
            }

            else
            {
                if (fire.activeSelf)
                {
                    fire.SetActive(false);
                }
            }
        }
        

        //ICE
        if (iceCubesList.Count > 0)
        {
            if (selfStats.ice[SELF_INTENSITY] > 0)
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

                float iceScale = selfStats.ice[SELF_INTENSITY];

                iceCube.transform.localScale = new Vector3(iceScale, iceScale, iceScale);

                if (animator != null && agent != null)
                {
                    animator.speed = 0;
                    agent.speed = 0;
                }

                frozen = true;
            }

            else if (frozen)
            {
                frozen = false;

                foreach (GameObject iceCube in iceCubesList)
                {
                    iceCube.SetActive(false);
                }

                animator.speed = 1;
                agent.speed = baseSpeed;
            }
        }
    }

    #endregion
    //========================


}

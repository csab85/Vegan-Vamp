using System.Collections;
using UnityEngine;

public class Respanw : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //components
    Animator animator;

    //scripts
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [HideInInspector] public Vector3 spawnPoint;
    float baseHealth;
    bool respawning = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);

        animator.SetLayerWeight(AnimationConsts.DAMAGE_LAYER, 0);

        float actualHealth = selfStats.health[StatsConst.SELF_INTENSITY];
        float healthToSum = baseHealth - actualHealth;

        selfStats.ApplyToBase(StatsConst.HEALTH, healthToSum);

        transform.position = spawnPoint;

        selfStats.dead = false;
        respawning = false;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        //get components
        animator = GetComponent<Animator>();

        //get scripts
        selfStats = GetComponent<StatsManager>();

        //get values
        spawnPoint = transform.position;
        baseHealth = selfStats.health[StatsConst.SELF_INTENSITY];
    }

    private void Update()
    {
        if (selfStats.dead && !respawning)
        {
            StartCoroutine(Respawn());
            respawning = true;
        }
    }

    #endregion
    //========================


}

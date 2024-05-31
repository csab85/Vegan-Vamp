using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    RandomWalk randomWalk;
    FieldOfView fov;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    [Header ("Time Settings")]
    [SerializeField] float resetTime;

    [Header ("Normal Movement Settings")]
    [SerializeField] float baseSpeed;
    [SerializeField] float baseVisionRange;
    [SerializeField] float baseAttackRange;
    [SerializeField] float baseVisionAngle;

    [Header ("Chasing Movement Settings")]
    [SerializeField] float fleeingSpeed;
    [SerializeField] float fleeingVisionRange;
    [SerializeField] float fleeingAttackRange;
    [SerializeField] float fleeingVisionAngle;

    [Header ("Headbutt Settings")]
    [SerializeField] LayerMask obstacleLayer;

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] bool fleeing;
    [SerializeField] bool reseting;

    Vector3 playerPosit;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator ResetState()
    {
        reseting = true;
        yield return new WaitForSeconds(resetTime);
        fleeing = false;
        reseting = false;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        randomWalk = GetComponent<RandomWalk>();
        fov = GetComponent<FieldOfView>();

        //get base values
        baseSpeed = navMeshAgent.speed;
        baseVisionRange = fov.visionRadius;
        baseAttackRange = fov.attackRadius;
        baseVisionAngle = fov.angle;
    }

    void Update()
    {
        if (fleeing)
        {
            //Enhancements
            if (navMeshAgent.speed < fleeingSpeed)
            {
                    print("enhance");
                    navMeshAgent.speed = fleeingSpeed;
                    fov.visionRadius = fleeingVisionRange;
                    fov.attackRadius = fleeingAttackRange;
                    fov.angle = fleeingVisionAngle;
            }

            //run away
            playerPosit = player.transform.position;
                    
            float distance = Vector3.Distance(playerPosit, transform.position);

            if  (distance < fov.visionRadius)
            {
                if (reseting)
                {
                    StopCoroutine(ResetState());
                    reseting = false;
                }

                Vector3 direction = (playerPosit - transform.position).normalized;

                float runDistance = fov.visionRadius + 2;

                navMeshAgent.destination = transform.position - (runDistance * direction);
            }

            else
            {
                StartCoroutine(ResetState());
            }
        }

        //random walk if not fleeing
        else if (navMeshAgent.remainingDistance <= 0.1f)
        {
            randomWalk.MoveToRandomPosit();
        }

        //start fleeing if seeing
        if (fov.isSeeingPlayer && !fleeing)
        {
            fleeing = true;
        }

        //go to normal speed if not fighting
        if (navMeshAgent.speed > baseSpeed && !fleeing)
        {
            print("decrease");
            navMeshAgent.speed = baseSpeed;
            fov.visionRadius = baseVisionRange;
            fov.attackRadius = baseAttackRange;
            fov.angle = baseVisionAngle;
        }

        //coeficient of speed (from stat manager)

        // if (StatsManager.speedCoeficient != 0 && navMeshAgent.speed != Manager.speedBase * spedCOeficient)
        // {
        //     navMeshAgent.speed *= ManagedReferenceMissingType.speedcoeficient
        // }
    }

    #endregion
    //========================


}

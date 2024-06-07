using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class BasicBehaviour : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    GameObject player;

    //components
    Animator animator;
    NavMeshAgent agent;

    //script
    RandomWalk randomWalk;
    RandomFlight randomFlight;
    FieldOfView fov;
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Chasing Movement Settings")]
    [SerializeField] float chasingSpeed;
    [SerializeField] float chasingVisionRange;
    [SerializeField] float chasingAttackRange;
    [SerializeField] float chasingVisionAngle;
    [SerializeField] bool flying;

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] public AlertState alertState;

    float baseSpeed;
    float baseVisionRange;
    float baseAttackRange;
    float baseVisionAngle;

    Vector3 playerPosit;

    public enum AlertState
    {
        Chilling,
        Searching,
        Fighting
    }

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
        //get components
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //get scripts
        TryGetComponent<RandomWalk>(out randomWalk);
        TryGetComponent<RandomFlight>(out randomFlight);
        fov = GetComponent<FieldOfView>();
        selfStats = GetComponent<StatsManager>();

        //get game objects
        player = fov.player;

        //get base values
        baseSpeed = agent.speed;
        baseVisionRange = fov.visionRadius;
        baseAttackRange = fov.attackRadius;
        baseVisionAngle = fov.angle;
    }

    void Update()
    {
        switch (alertState)
        {
            case AlertState.Chilling:

                //go to normal speed
                if (agent.speed != baseSpeed * selfStats.speedMultiplier)
                {
                    print("decrease");
                    agent.speed = baseSpeed;
                    fov.visionRadius = baseVisionRange;
                    fov.attackRadius = baseAttackRange;
                    fov.angle = baseVisionAngle;
                }

                //walk or fly randomly
                if (agent.remainingDistance <= 0.1f)
                {
                    if (flying)
                    {
                        randomFlight.MoveToRandomPosit();
                    }

                    else
                    {
                        randomWalk.MoveToRandomPosit();
                    }
                }

                //start searching if seeing
                if (fov.isSeeingPlayer)
                {
                    alertState = AlertState.Searching;
                }

                break;
            
            case AlertState.Searching:

                if (agent.speed != chasingSpeed * selfStats.speedMultiplier)
                {
                    print("enhance");
                    agent.speed = chasingSpeed;
                    fov.visionRadius = baseVisionRange;
                    fov.attackRadius = baseAttackRange;
                    fov.angle = baseVisionAngle;
                }

                playerPosit = player.transform.position;

                //follow player if seeing
                if (fov.isSeeingPlayer)
                {
                    agent.destination = playerPosit;
                }

                //if not seeing and on last seen posit, walk randomly
                else if (agent.remainingDistance < 0.1f)
                {
                    alertState = AlertState.Chilling;
                }

                //attack if close enough
                if (fov.isInAttackRange && fov.isSeeingPlayer)
                {
                    alertState = AlertState.Fighting;
                }

                break;

        }
    }

    #endregion
    //========================


}

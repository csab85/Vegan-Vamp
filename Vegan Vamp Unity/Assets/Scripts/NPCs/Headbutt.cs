using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class Headbutt : MonoBehaviour
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
    [SerializeField] float aimTime;
    [SerializeField] float stunTime;

    [Header ("Normal Movement Settings")]
    [SerializeField] float baseSpeed;
    [SerializeField] float baseVisionRange;
    [SerializeField] float baseAttackRange;
    [SerializeField] float baseVisionAngle;

    [Header ("Chasing Movement Settings")]
    [SerializeField] float chasingSpeed;
    [SerializeField] float chasingVisionRange;
    [SerializeField] float chasingAttackRange;
    [SerializeField] float chasingVisionAngle;

    [Header ("Headbutt Settings")]
    [SerializeField] float headbuttForce;
    [SerializeField] float headbuttDistance;
    [SerializeField] float headbuttingSpeed;
    [SerializeField] LayerMask obstacleLayer;
    
    float circleDistance;

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] States actualState;
    [SerializeField] bool fighting;
    [SerializeField] bool aiming;
    [SerializeField] bool waiting;

    Vector3 playerPosit;

    //circling
    List<Vector3> circlePoints = new List<Vector3>();

    enum States
    {
        Searching,
        Aiming,
        Headbutting,
        Waiting,
        Stunned
    }

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    //vou guardar pq vai que ne
    void Circle()
    {
        float angle = navMeshAgent.speed * Time.time;
        float circleX = Mathf.Cos(angle) * circleDistance;
        float circleZ = Mathf.Sin(angle) * circleDistance;

        circlePoints.Add(playerPosit + new Vector3(circleX, 0, circleZ));
        navMeshAgent.destination = circlePoints[0];
    }

    IEnumerator Aim()
    {
        aiming = true;
        yield return new WaitForSecondsRealtime(aimTime); 
        actualState = States.Headbutting;      
        aiming = false;
    }

    IEnumerator Wait(float waitTime)
    {
        
        waiting = true;
        navMeshAgent.speed = chasingSpeed;

        yield return new WaitForSecondsRealtime(waitTime);
        
        waiting = false;
        actualState = States.Searching;
    }

    void OnCollisionEnter(Collision other)
    {
        if (fighting)
        {
            if (other.gameObject.tag == "Player")
            {
                StopCoroutine(Wait(1));

                Vector3 headbuttDirection = (playerPosit - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(headbuttDirection * headbuttForce / 2, ForceMode.Impulse);
            }

            else if (other.gameObject.layer == obstacleLayer)
            {
                actualState = States.Stunned;
            }
        }
        
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
        if (fighting)
        {
            //Enhancements
            if (navMeshAgent.speed < chasingSpeed && actualState != States.Headbutting)
            {
                    print("enhance");
                    navMeshAgent.speed = chasingSpeed;
                    fov.visionRadius = baseVisionRange;
                    fov.attackRadius = baseAttackRange;
                    fov.angle = baseVisionAngle;
            }

            playerPosit = player.transform.position;


            switch (actualState)
            {
                case States.Searching:
                    
                    if (!navMeshAgent.autoBraking)
                    {
                        navMeshAgent.autoBraking = true;
                    }

                    //follow player if seeing
                    if (fov.isSeeingPlayer)
                    {
                        navMeshAgent.destination = playerPosit;
                    }

                    //attack if close enough
                    if (fov.isInAttackRange)
                    {
                        actualState = States.Aiming;
                    }

                    break;
                
                case States.Aiming:

                    //start aim countdown and look at player
                    if (!aiming)
                    {
                        navMeshAgent.destination = transform.position;
                        StartCoroutine(Aim());
                    }

                    transform.LookAt(playerPosit);

                    break;

                case States.Headbutting:

                    //set to headbutt speed and set destination past player
                    if (navMeshAgent.speed != headbuttingSpeed)
                    {
                        navMeshAgent.speed = headbuttingSpeed;
                        navMeshAgent.destination = playerPosit + (headbuttDistance * transform.forward);
                    }

                    if (navMeshAgent.remainingDistance < 0.1f)
                    {
                        actualState = States.Waiting;
                    }

                    break;

                case States.Waiting:

                    if (!waiting)
                    {
                        StartCoroutine(Wait(1));
                    }

                    break;

                case States.Stunned:

                    if (!waiting)
                    {
                        StartCoroutine(Wait(stunTime));
                    }

                    break;
            }
        }

        //random walk if not fighting
        else if (navMeshAgent.remainingDistance <= 0.1f)
        {
            randomWalk.MoveToRandomPosit();
        }

        //start searching if seeing
        if (fov.isSeeingPlayer && !fighting)
        {
            fighting = true;
            actualState = States.Searching;
        }

        //go to normal speed if not fighting
        if (navMeshAgent.speed > baseSpeed && !fighting)
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

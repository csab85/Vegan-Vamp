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
    Animator animator;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    [Header ("Time Settings")]
    [SerializeField] float aimTime;
    [SerializeField] float stunTime;

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

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] States actualState;
    [SerializeField] bool fighting;
    [SerializeField] bool aiming;
    [SerializeField] bool waiting;

    float baseSpeed;
    float baseVisionRange;
    float baseAttackRange;
    float baseVisionAngle;

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

    /// <summary>
    /// The object will look at the table for aim time seconds then go to state headbutting
    /// </summary>
    /// <returns></returns>
    IEnumerator Aim()
    {
        aiming = true;
        animator.SetBool ("Aiming", true);
        yield return new WaitForSeconds(aimTime); 
        actualState = States.Headbutting;      
        aiming = false;
        animator.SetBool ("Aiming", false);
    }

    /// <summary>
    /// Waits for a wait time seconds then goes into searching state
    /// </summary>
    /// <param name="waitTime">Seconds to wait</param>
    /// <returns></returns>
    IEnumerator Wait(float waitTime)
    {
        
        waiting = true;
        navMeshAgent.speed = chasingSpeed;

        yield return new WaitForSeconds(waitTime);
        
        waiting = false;
        actualState = States.Searching;
    }

    void OnCollisionEnter(Collision other)
    {
        if (fighting)
        {   
            //knockback player then wait
            if (other.gameObject.tag == "Player")
            {
                StopCoroutine(Wait(1));

                Vector3 headbuttDirection = (playerPosit - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(headbuttDirection * headbuttForce / 2, ForceMode.Impulse);
            }

            //get stunned
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
        animator = GetComponent<Animator>();

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

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class DivingChase : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    Rigidbody rb;
    Animator animator;
    NavMeshAgent navMeshAgent;
    RandomWalk randomWalk;
    FieldOfView fov;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Info")]
    [SerializeField] Vector3 targetPoint;
    [SerializeField] float targetDistance;
    [SerializeField] float distance;
    [SerializeField] States actualState;
    [SerializeField] public bool fighting;

    float baseSpeed;
    float baseVisionRange;
    float baseAttackRange;
    float baseVisionAngle;

    bool waiting = false;
    Vector3 playerPosit;

    enum States
    {
        Searching,
        Attacking,
        Waiting
    }

    bool waitingReduce;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator Wait(float waitTime)
    {
        
        waiting = true;

        yield return new WaitForSeconds(waitTime);
        
        waiting = false;
        animator.SetBool("Attacking", false);
        actualState = States.Searching;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region
    void Start()
    {
        //get components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
            distance = Vector3.Distance(transform.position, player.transform.position);
            playerPosit = player.transform.position;
            targetDistance = navMeshAgent.remainingDistance;

            switch (actualState)
            {
                case States.Searching:

                    //follow player if seeing
                    if (fov.isSeeingPlayer)
                    {
                        navMeshAgent.destination = playerPosit;
                        print(navMeshAgent.remainingDistance);
                    }

                    //attack if close enough
                    if (navMeshAgent.remainingDistance < 0.1f)
                    {
                        actualState = States.Attacking;
                    }

                    break;

                case States.Attacking:


                    animator.SetBool("Attacking", true);
                    actualState = States.Waiting;

                    break;

                case States.Waiting:

                    navMeshAgent.destination = transform.position;
                    StartCoroutine(Wait(2));

                    break;
            }


            navMeshAgent.destination = targetPoint;
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
    }

    #endregion
    //========================


}

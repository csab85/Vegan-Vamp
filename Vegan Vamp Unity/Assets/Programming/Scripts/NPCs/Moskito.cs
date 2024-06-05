using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Moskito : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    [SerializeField] GameObject projectile;
    Rigidbody rb;
    NavMeshAgent agent;
    RandomFlight randomFlight;
    RandomWalk randomWalk;
    FieldOfView fov;
    Animator animator;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float waitTime;

    [Header ("Chasing Movement Settings")]
    [SerializeField] float chasingSpeed;
    [SerializeField] float chasingVisionRange;
    [SerializeField] float chasingAttackRange;
    [SerializeField] float chasingVisionAngle;

    //pathfinding and State
    [Header ("Info")]
    [SerializeField] State actualState;
    [SerializeField] bool fighting;
    [SerializeField] bool aiming;
    [SerializeField] bool waiting;

    float baseSpeed;
    float baseVisionRange;
    float baseAttackRange;
    float baseVisionAngle;

    Vector3 playerPosit;

    enum State
    {
        Searching,
        Aim,
        Flying,
        Dragging,
        //maybe add wait here
        Shooting,
        Biting,
        Waiting,
        Burning,
    }

    [HideInInspector] public bool flying = true;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void Shoot()
    {
        transform.LookAt(playerPosit);
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, null);
        newProjectile.SetActive(true);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);

        StartCoroutine(Wait());
    }

    void Bite()
    {
        print("bite");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        actualState = State.Searching;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        randomFlight = GetComponent<RandomFlight>();
        fov = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();

        //get base values
        baseSpeed = agent.speed;
        baseVisionRange = fov.visionRadius;
        baseAttackRange = fov.attackRadius;
        baseVisionAngle = fov.angle;
    }

    void Update()
    {
        if (fighting)
        {
            //Enhancements
            if (agent.speed < chasingSpeed)
            {
                    print("enhance");
                    agent.speed = chasingSpeed;
                    fov.visionRadius = chasingVisionRange;
                    fov.attackRadius = chasingAttackRange;
                    fov.angle = chasingVisionAngle;
            }

            playerPosit = player.transform.position;

            //focus on flying around player
            randomFlight.areaCenter = playerPosit;

            switch (actualState)
            {
                case State.Searching:

                    if (fov.isSeeingPlayer)
                    {   
                        //attack player if seeing and close enough
                        if (fov.isInAttackRange)
                        {
                            actualState = State.Aim;
                        }

                        //if not close go to player
                        else
                        {
                            agent.destination = playerPosit;
                        }
                    }

                    //if not seeing and on last seen posit, walk randomly
                    else if (agent.remainingDistance < 0.1f)
                    {
                        fighting = false;
                    }

                    break;

                case State.Aim:

                    if (flying)
                    {
                        randomFlight.MoveToRandomPosit();

                        if (Vector3.Distance(agent.destination, playerPosit) > 5)
                        {
                            actualState = State.Flying;
                        }
                    }

                    else
                    {
                        actualState = State.Dragging;
                    }

                    break;

                case State.Flying:

                    if (agent.remainingDistance < 0.1f)
                    {
                        actualState = State.Shooting;
                    }

                    break;

                case State.Dragging:

                    agent.destination = playerPosit;

                    if (agent.remainingDistance < 0.5f)
                    {
                        actualState = State.Biting;
                    }

                    break;

                case State.Shooting:

                    Shoot();
                    actualState = State.Waiting;

                    break;

                case State.Biting:

                    Bite();
                    actualState = State.Waiting;

                    break;

                case State.Waiting:

                    //varios nadas acontecendo aqui

                    break;

                case State.Burning:

                    if (agent.remainingDistance < 0.1f)
                    {
                        if (flying)
                        {
                            randomFlight.areaCenter = transform.position + randomFlight.offset;
                            randomFlight.MoveToRandomPosit();
                        }

                        else
                        {
                            randomWalk.MoveToRandomPosit();
                        }
                    }

                    break;
            }
        }

    //random walk if not fighting
        else if (agent.remainingDistance <= 0.1f)
        {
            if (flying)
            {
                randomFlight.areaCenter = transform.position + randomFlight.offset;
                randomFlight.MoveToRandomPosit();
            }

            else
            {
                randomWalk.MoveToRandomPosit();
            }
            
        }

        //start searching if seeing
        if (fov.isSeeingPlayer && !fighting)
        {
            fighting = true;
            actualState = State.Searching;
        }

        //go to normal speed if not fighting
        if (agent.speed > baseSpeed && !fighting)
        {
            print("decrease");
            agent.speed = baseSpeed;
            fov.visionRadius = baseVisionRange;
            fov.attackRadius = baseAttackRange;
            fov.angle = baseVisionAngle;
        }
    }

    #endregion
    //========================


}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Moskito : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //Game objects
    GameObject player;
    [SerializeField] GameObject projectile;

    //components
    Animator animator;
    NavMeshAgent agent;

    //script
    RandomWalk randomWalk;
    RandomFlight randomFlight;
    FieldOfView fov;
    BasicBehaviour basicBehaviour;

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

    Vector3 playerPosit;

    enum State
    {
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
        basicBehaviour.alertState = BasicBehaviour.AlertState.Searching;
        actualState = State.Aim;
    }

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
        randomWalk = GetComponent<RandomWalk>();
        randomFlight = GetComponent<RandomFlight>();
        fov = GetComponent<FieldOfView>();
        basicBehaviour = GetComponent<BasicBehaviour>();

        //get game objects
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (basicBehaviour.alertState == BasicBehaviour.AlertState.Fighting)
        {
            playerPosit = player.transform.position;

            //focus on flying around player
            randomFlight.areaCenter = playerPosit;

            switch (actualState)
            {
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
    }

    #endregion
    //========================


}

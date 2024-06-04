using UnityEngine;
using UnityEngine.AI;

public class Moskito : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    RandomFlight randomFlight;
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

    enum States
    {
        Searching,
        Flying,
        Dragging,
        //maybe add wait here
        Shooting,
        Biting,
        Waiting,
        Burning,
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
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        randomFlight = GetComponent<RandomFlight>();
        fov = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();

        //get base values
        baseSpeed = navMeshAgent.speed;
        baseVisionRange = fov.visionRadius;
        baseAttackRange = fov.attackRadius;
        baseVisionAngle = fov.angle;
    }

    #endregion
    //========================


}

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
    
    [Header ("Settings")]
    [SerializeField] float aimTime;
    [SerializeField] float headbuttForce;
    [SerializeField] float speedEnhance;
    [SerializeField] float visionRangeEnhance;
    [SerializeField] float visionAngleEnhance;
    
    float circleDistance;
    float distanceThreshold;

    //pathfinding and states
    public bool fighting;
    public bool aiming;
    Vector3 playerPosit;

    //chasing settings
    float baseSpeed;
    float baseVision;

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

    [SerializeField] States actualState;

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

    void OnCollisionEnter(Collision other)
    {
        if (actualState == States.Headbutting)
        {
            actualState = States.Searching;
        }

        if (other.gameObject.tag == "Player")
        {
            Vector3 headbuttDirection = (playerPosit - transform.position).normalized;

            other.gameObject.GetComponent<Rigidbody>().AddForce(headbuttDirection * headbuttForce / 2, ForceMode.Impulse);
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
    }

    void Update()
    {
        if (fighting)
        {
            //Enhancements
            if (navMeshAgent.speed != speed * speedEnhance)
            {
                navMeshAgent.speed *= speedEnhance;

            }

            playerPosit = player.transform.position;


            switch (actualState)
            {
                case States.Searching:

                    if (!navMeshAgent.enabled)
                    {
                        navMeshAgent.enabled = true;
                    }

                    if (fov.isSeeingPlayer)
                    {
                        actualState = States.Aiming;
                    }

                    break;
                
                case States.Aiming:

                    if (!aiming)
                    {
                        navMeshAgent.destination = transform.position;
                        StartCoroutine(Aim());
                    }

                    transform.LookAt(playerPosit);

                    break;

                case States.Headbutting:

                        navMeshAgent.enabled = false;
                        rb.AddForce((playerPosit - transform.position).normalized * headbuttForce, ForceMode.Impulse);

                        actualState = States.Waiting;

                    break;

                case States.Waiting:
                    break;
            }
        }

        else if (navMeshAgent.remainingDistance <= 0.1f)
        {
            randomWalk.MoveToRandomPosit();
        }

        if (fov.isSeeingPlayer && !fighting)
        {
            fighting = true;
            actualState = States.Searching;
        }
    }

    #endregion
    //========================


}

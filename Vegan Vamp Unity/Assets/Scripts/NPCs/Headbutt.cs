using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Headbutt : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    NavMeshAgent navMeshAgent;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    [Header ("Settings")]
    [SerializeField] float circleDistance;
    [SerializeField] float speed;
    [SerializeField] float distanceThreshold;

    public bool fighting;
    Vector3 targetPoint;
    Vector3 playerPosit;

    //circling
    List<Vector3> circlePoints = new List<Vector3>();

    enum States
    {
        Circling,
        Walking,
        Aiming,
        Headbutting,
        Stunned
    }

    [SerializeField] States actualState = States.Walking;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Circle()
    {
        float angle = speed * Time.time;
        float circleX = Mathf.Cos(angle) * circleDistance;
        float circleZ = Mathf.Sin(angle) * circleDistance;

        circlePoints.Add(playerPosit + new Vector3(circleX, 0, circleZ));
        targetPoint = circlePoints[0];
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        if (fighting)
        {

            playerPosit = player.transform.position;
            navMeshAgent.destination = targetPoint;


            switch (actualState)
            {
                case States.Circling:

                    Circle();

                    if (navMeshAgent.remainingDistance < distanceThreshold)
                    {
                        circlePoints.Remove(circlePoints[0]);
                    }
                
                    break;

                case States.Aiming:

                    break;
            }
        }
    }

    #endregion
    //========================


}

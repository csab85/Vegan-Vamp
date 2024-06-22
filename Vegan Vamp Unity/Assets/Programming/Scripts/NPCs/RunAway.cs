using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    GameObject player;

    //components
    Animator animator;
    NavMeshAgent navMeshAgent;

    //scripts
    FieldOfView fov;
    BasicBehaviour basicBehaviour;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    [Header ("Settings")]
    [SerializeField] float fleeDistance;
    [SerializeField] float resetTime;

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] bool reseting;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator ResetState()
    {
        reseting = true;
        yield return new WaitForSeconds(resetTime);
        basicBehaviour.alertState = BasicBehaviour.AlertState.Chilling;
        reseting = false;
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
        navMeshAgent = GetComponent<NavMeshAgent>();

        //get scripts
        fov = GetComponent<FieldOfView>();
        basicBehaviour = GetComponent<BasicBehaviour>();

        //get game objects
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (basicBehaviour.alertState == BasicBehaviour.AlertState.Fighting)
        {
            Vector3 playerPosit = player.transform.position;
                    
            float distance = Vector3.Distance(playerPosit, transform.position);

            //if player within vision
            if  (distance < fov.visionRadius)
            {
                //stop reseting if reseting
                if (reseting)
                {
                    StopCoroutine(ResetState());
                    reseting = false;
                }

                Vector3 direction = (playerPosit - transform.position).normalized;

                float runDistance = fov.visionRadius + fleeDistance;

                navMeshAgent.destination = transform.position - (runDistance * direction);
            }

            //start resetting if not on vision
            else
            {
                if (!reseting)
                {
                    StartCoroutine(ResetState());
                }
            }
        }
    }

    #endregion
    //========================


}

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class CarefulChase : MonoBehaviour
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
    [SerializeField] float initialDistance;
    [Tooltip ("How much closer they get to player every approach cooldown")]
    [SerializeField] float toReduceDistance;
    [Tooltip ("How close to the player they must be to start attacking")]
    [SerializeField] float attackDistance;
    [Tooltip ("How many seconds they wait after getting a bit closer")]
    [SerializeField] float approachCooldown;
    [Tooltip ("How many seconds they'll stay attacking the player")]
    [SerializeField] float attackDuration;
    [Tooltip ("How further from the initial distance they should go after finished attacking")]
    [SerializeField] float distanceThreshold;

    [Header ("Info")]
    [SerializeField] Vector3 targetPoint;
    [SerializeField] float targetDistance;
    [SerializeField] float distance;
    [SerializeField] states actualState;
    [SerializeField] public bool fighting;

    Vector3 playerPosit;

    enum states
    {
        locking,
        reducing,
        approaching,
        attacking,
        retreating
    }

    bool waitingReduce;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void LockOn()
    {
        if (navMeshAgent.speed != 3.5f)
        {
            navMeshAgent.speed = 3.5f;
        }

        targetPoint = playerPosit + (transform.position - playerPosit).normalized * initialDistance;

        if (targetDistance != initialDistance)
        {
            targetDistance = initialDistance;
        }
    }

    IEnumerator ReduceDistance()
    {
        waitingReduce = true;
        yield return new WaitForSeconds(approachCooldown);
        targetDistance -= toReduceDistance;
        waitingReduce = false;
    }

    void Approach()
    {
        targetPoint = playerPosit + (transform.position - playerPosit).normalized * targetDistance;
    }

    IEnumerator Attack()
    {
        navMeshAgent.speed = 5;
        yield return new WaitForSeconds(attackDuration);
        actualState = states.retreating;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        LockOn();
    }

    void Update()
    {
        

        if (fighting)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            playerPosit = player.transform.position;

            switch (actualState)
            {
                case states.locking:

                    LockOn();

                    if (distance - distanceThreshold <= initialDistance)
                    {
                        actualState = states.reducing;
                    }

                    break;

                case states.reducing:

                    StartCoroutine(ReduceDistance());
                    
                    actualState = states.approaching;

                    break;

                case states.approaching:
                    
                    if (distance - distanceThreshold <= attackDistance)
                    {
                        actualState = states.attacking;

                        break;
                    }

                    if (distance - distanceThreshold <= targetDistance && !waitingReduce)
                    {
                        actualState = states.reducing;

                        break;
                    }

                    Approach();

                    break;

                case states.attacking:

                    StartCoroutine(Attack());
                    targetPoint = playerPosit;

                    break;

                case states.retreating:

                    targetPoint = playerPosit + (transform.position - playerPosit).normalized * initialDistance;

                    if (distance + distanceThreshold > targetDistance)
                    {
                        actualState = states.locking;
                    }

                    break;
            }


            navMeshAgent.destination = targetPoint;
        }

        
    }

    #endregion
    //========================


}

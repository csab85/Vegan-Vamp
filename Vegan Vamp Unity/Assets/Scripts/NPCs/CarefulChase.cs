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
    [SerializeField] Vector3 initialDistance;
    [Tooltip ("How much closer they get to player every approach cooldown")]
    [SerializeField] Vector3 toReduceDistance;
    [Tooltip ("How close to the player they must be to start attacking")]
    [SerializeField] float attackDistance;
    [Tooltip ("How many seconds they wait after getting a bit closer")]
    [SerializeField] float approachCooldown;
    [Tooltip ("How many seconds they'll stay attacking the player")]
    [SerializeField] float attackDuration;
    [Tooltip ("How further from the initial distance they should go after finished attacking")]
    [SerializeField] float retreatExtraDistance;

    [Header ("Info")]
    [SerializeField] Vector3 targetPoint;
    [SerializeField] Vector3 targetDistance;
    [SerializeField] float distance;
    [SerializeField] states actualState;

    Vector3 playerPosit;

    enum states
    {
        locking,
        reducing,
        approaching,
        attacking,
        retreating
    }

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void LockOn()
    {
        navMeshAgent.speed = 3.5f;
        targetPoint = playerPosit + initialDistance;

        if (targetDistance != initialDistance)
        {
            targetDistance = initialDistance;
        }
    }

    IEnumerator ReduceDistance()
    {
        yield return new WaitForSecondsRealtime(approachCooldown);
        targetDistance -= toReduceDistance;
    }

    void Approach()
    {
        targetPoint = playerPosit - targetDistance;
    }

    IEnumerator Attack()
    {
        navMeshAgent.speed = 5;
        yield return new WaitForSecondsRealtime(attackDuration);
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
        distance = Vector3.Distance(transform.position, player.transform.position);
        playerPosit = player.transform.position;

        switch (actualState)
        {
            case states.locking:

                LockOn();

                if (distance <= initialDistance.magnitude)
                {
                    actualState = states.reducing;
                }

                break;

            case states.reducing:

                StartCoroutine(ReduceDistance());
                Approach();

                actualState = states.approaching;

                break;

            case states.approaching:
                
                if (distance <= attackDistance)
                {
                    actualState = states.attacking;

                    break;
                }

                if (distance <= targetDistance.magnitude)
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

                targetPoint = playerPosit + initialDistance;

                if (distance > targetDistance.magnitude + retreatExtraDistance)
                {
                    actualState = states.locking;
                }

                break;
        }

        navMeshAgent.destination = targetPoint;
    }

    #endregion
    //========================


}

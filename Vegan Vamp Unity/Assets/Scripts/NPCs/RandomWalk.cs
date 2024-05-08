using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Vector3 targetPosition;
    bool isMoving = false;
    float minDistance = 5f;
    float maxDistance = 10f;
    float minWaitTime = 1f;
    float maxWaitTime = 3f;
    float nextMoveTime = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        if (!isMoving && Time.time >= nextMoveTime)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas);
        targetPosition = hit.position;

        navMeshAgent.SetDestination(targetPosition);
        isMoving = true;
        nextMoveTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isMoving = false;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    NavMeshAgent navMeshAgent;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Area Settings")]
    [SerializeField] Vector3 areaCenter;
    [SerializeField] float areaRadius;

    [Header ("Agent Settings")]
    [SerializeField] float speed;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void MoveToRandomPosit()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(0, areaRadius);

        float circleX = areaCenter.x + Mathf.Cos(angle) * distance;
        float circleZ = areaCenter.z + Mathf.Sin(angle) * distance;

        Vector3 targetPosit = new Vector3(circleX, 0, circleZ);

        navMeshAgent.destination = targetPosit;
        print(targetPosit);
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
        MoveToRandomPosit();
    }

    #endregion
    //========================


}

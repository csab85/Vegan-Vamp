using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Quadratilos : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    NavMeshAgent navMeshAgent;
    [SerializeField] GameObject bathPoint;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] public bool dirty;
    Vector3 startingPoint;

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
        startingPoint = transform.position;
    }

    void Update()
    {
        if (dirty)
        {
            navMeshAgent.destination = bathPoint.transform.position;
        }

        if (!dirty)
        {
            navMeshAgent.destination = startingPoint;
        }
    }

    #endregion
    //========================


}

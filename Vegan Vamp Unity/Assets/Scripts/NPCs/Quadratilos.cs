using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Quadratilos : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    NavMeshAgent navMeshAgent;
    StatsManager statsManager;
    [SerializeField] GameObject bathPoint;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
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
        statsManager = GetComponent<StatsManager>();
        startingPoint = transform.position;
    }

    void Update()
    {
        if (statsManager.dirty.intensity > 0)
        {
            navMeshAgent.destination = bathPoint.transform.position;
        }

        else
        {
            navMeshAgent.destination = startingPoint;
        }
    }

    #endregion
    //========================


}

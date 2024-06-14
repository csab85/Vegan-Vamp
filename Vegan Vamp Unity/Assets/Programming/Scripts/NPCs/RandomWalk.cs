using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    NavMeshAgent navMeshAgent;
    Animator animator;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] public float areaRadius;
    [HideInInspector] public Vector3 areaCenter;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void MoveToRandomPosit()
    {
        animator.SetBool("Walking", true);

        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(0, areaRadius);

        float circleX = areaCenter.x + Mathf.Cos(angle) * distance;
        float circleZ = areaCenter.z + Mathf.Sin(angle) * distance;

        Vector3 targetPosit = new Vector3(circleX, 0, circleZ);

        navMeshAgent.destination = targetPosit;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        areaCenter = transform.position;
    }

    #endregion
    //========================


}

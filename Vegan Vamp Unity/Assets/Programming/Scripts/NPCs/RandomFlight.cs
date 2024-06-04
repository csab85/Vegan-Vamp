using UnityEngine;
using UnityEngine.AI;

public class RandomFlight : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    NavMeshAgent agent;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] public float areaRadius;
    [SerializeField] public float areaHeight;
    [SerializeField] public Vector3 offset;
    [HideInInspector] public Vector3 areaCenter;

    float height;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void MoveToRandomPosit()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(0, areaRadius);
        height = Random.Range(0, height);

        float circleX = areaCenter.x + Mathf.Cos(angle) * distance;
        float circleZ = areaCenter.z + Mathf.Sin(angle) * distance;

        Vector3 targetPosit = new Vector3(circleX, 0, circleZ);

        agent.destination = targetPosit;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        areaCenter = transform.position + offset;

        MoveToRandomPosit();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            MoveToRandomPosit();
        }

        if (agent.baseOffset != height)
        {
            agent.baseOffset = Mathf.MoveTowards(agent.baseOffset, height, 1 * Time.deltaTime);
        }
    }

    #endregion
    //========================


}

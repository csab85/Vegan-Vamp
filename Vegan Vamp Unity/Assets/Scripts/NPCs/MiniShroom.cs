using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniShroom : MonoBehaviour
{

    RandomWalk randomWalk;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        randomWalk = GetComponent<RandomWalk>();
        agent = GetComponent<NavMeshAgent>();
        randomWalk.MoveToRandomPosit();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < .1f)
        {
            randomWalk.MoveToRandomPosit();
        }
    }
}

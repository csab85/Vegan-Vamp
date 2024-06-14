using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    public GameObject player;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header("Settings")]
    [SerializeField] public float visionRadius;
    [SerializeField] public float attackRadius;

    [Range(0, 360)]
    [SerializeField] public float angle;

    [Header("Info")]
    [SerializeField] public bool isSeeingPlayer;
    [SerializeField] public bool isInAttackRange;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void FieldOfViewCheck()
    {
        //get colliders within range (only returns player collider) *THIS ONLY WORKS IF VISION RADIUS >= ATTACK RADIUS
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, visionRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //see if player is within field of view angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //check if there's obstacles blocking vision
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    isSeeingPlayer = true;

                    if (distanceToTarget <= attackRadius)
                    {
                        isInAttackRange = true;
                    }
                }

                else
                {
                    isSeeingPlayer = false;
                    isInAttackRange = false;
                }
            }

            else
            {
                isSeeingPlayer = false;
            }
        }

        else if (isSeeingPlayer == true)
        {
            isSeeingPlayer = false;
        }
    }

    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfViewCheck();
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        player = GameObject.Find("Player");
        print(player);
        StartCoroutine(FOVRoutine());
    }

    #endregion
    //========================


}

using UnityEngine;
using UnityEngine.AI;

public class MoskitFloor : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject attackingIcon;
    [SerializeField] Transform leaderPoint;
    Transform player;

    //components
    NavMeshAgent agent;
    Animator animator;

    //scripts
    StatsManager selfStats;
    RandomWalk randomWalk;
    FieldOfView fov;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float damage;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnCollisionEnter(Collision collision)
    {
        //apply dmg
        StatsEffects enemyEffects = collision.gameObject.GetComponent<StatsEffects>();

        if (enemyEffects != null)
        {
            if (collision.gameObject.tag == "Player" && !selfStats.dead)
            {
                Vector3 direction = (collision.transform.position - transform.position).normalized;

                enemyEffects.DamageSelf(direction, damage);
            }
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        //get transform
        player = GameObject.Find("Player").transform;

        //get components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //get scripts
        selfStats = GetComponent<StatsManager>();
        randomWalk = GetComponent<RandomWalk>();
        fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        //if not seeing player
        if (!fov.isSeeingPlayer)
        {
            //move to leader if not null
            if (leaderPoint != null)
            {
                agent.destination = leaderPoint.position;
            }

            //move to random posit if no leader
            else
            {
                randomWalk.MoveToRandomPosit();
            }

            //deactivate sign
            if (attackingIcon.activeSelf)
            {
                attackingIcon.SetActive(false);
            }
        }

        //if seeing player
        if (fov.isSeeingPlayer)
        {
            //move to player
            agent.destination = player.position;

            //activate icon
            if (!attackingIcon.activeSelf)
            {
                attackingIcon.SetActive(true);
            }
        }
    }

    #endregion
    //========================


}

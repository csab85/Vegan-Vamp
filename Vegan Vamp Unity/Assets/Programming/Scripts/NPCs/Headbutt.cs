using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class Headbutt : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    GameObject player;

    //components
    Animator animator;
    Rigidbody rb;
    NavMeshAgent agent;

    //scripts
    FieldOfView fov;
    BasicBehaviour basicBehaviour;
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    [Header ("Time Settings")]
    [SerializeField] float waitTime;

    [Header ("Headbutt Settings")]
    [SerializeField] float headbuttForce;
    [SerializeField] float headbuttDistance;
    [SerializeField] float headbuttingSpeed;
    [SerializeField] LayerMask obstacleLayer;

    //pathfinding and states
    [Header ("Info")]
    [SerializeField] FightState fightState;
    [SerializeField] bool fighting;
    [SerializeField] bool aiming;
    [SerializeField] bool waiting;

    Vector3 playerPosit;

    enum FightState
    {
        Aiming,
        Headbutting,
        Waiting,
        Stunned
    }

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Attack()
    {
        fightState = FightState.Headbutting;
        aiming = false;

        agent.destination = playerPosit + (headbuttDistance * transform.forward * 1.5f);
    }

    /// <summary>
    /// Waits for a wait time seconds then goes into searching state
    /// </summary>
    /// <param name="waitTime">Seconds to wait</param>
    /// <returns></returns>
    IEnumerator Wait(float waitTime)
    {
        waiting = true;

        yield return new WaitForSeconds(waitTime);
        
        waiting = false;
        basicBehaviour.alertState = BasicBehaviour.AlertState.Searching;
        fightState = FightState.Aiming;
    }

    void OnCollisionEnter(Collision other)
    {
        if (fighting)
        {   
            //knockback player then wait
            if (other.gameObject.tag == "Player")
            {
                StopCoroutine(Wait(1));

                Vector3 headbuttDirection = (playerPosit - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(headbuttDirection * headbuttForce / 2, ForceMode.Impulse);

                //knockbackl self
                agent.destination = transform.position;
                rb.AddForce(-headbuttDirection * headbuttForce / 2, ForceMode.Impulse);
                animator.Play("Break");
                print("irrinho");
            }
        }
        
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        //get scripts
        fov = GetComponent<FieldOfView>();
        basicBehaviour = GetComponent<BasicBehaviour>();
        selfStats = GetComponent<StatsManager>();

        //get game objects
        player = fov.player;
    }

    void Update()
    {
        if (basicBehaviour.alertState == BasicBehaviour.AlertState.Fighting && !selfStats.dead)
        {
            playerPosit = player.transform.position;

            switch (fightState)
            {                
                case FightState.Aiming:

                    //start aim countdown and look at player
                    if (!aiming)
                    {
                        agent.destination = transform.position;
                        animator.Play("Startup");
                        aiming = true;
                    }

                    transform.LookAt(playerPosit);

                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                    //reset if stuck on a animation
                    if (stateInfo.IsName("Idle") | stateInfo.IsName("Walking"))
                    {
                        aiming = false;
                        animator.SetBool("Walking", false);
                    }

                    break;

                case FightState.Headbutting:

                    //set to headbutt speed and set destination past player
                    if (agent.speed != headbuttingSpeed * selfStats.speedMultiplier)
                    {
                        agent.speed = headbuttingSpeed * selfStats.speedMultiplier;

                        //play animation
                        animator.Play("Attacking");
                    }

                    if (agent.remainingDistance < 0.05f)
                    {
                        fightState = FightState.Waiting;
                    }

                    break;

                case FightState.Waiting:

                    if (!waiting)
                    {
                        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                        if (!stateInfo.IsName("Break"))
                        {
                            animator.Play("Idle");
                            StartCoroutine(Wait(waitTime));
                        }
                    }

                    break;
            }
        }
    }

    #endregion
    //========================


}

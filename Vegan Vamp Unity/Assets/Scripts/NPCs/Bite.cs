using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{

    [SerializeField] GameObject player;
    FieldOfView fov;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);

        if (fov.isSeeingPlayer)
        {
            animator.SetBool("Attacking", true);
        }

        else
        {
            animator.SetBool("Attacking", false);
        }
    }
}

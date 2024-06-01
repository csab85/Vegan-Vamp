using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Imports")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPool;
    [SerializeField] GameObject aimColliders;
    [SerializeField] GameObject muzzle;

    Animator animator;
    VisualEffect muzzleFX;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header("Settings")]
    [SerializeField] int capacity;
    [SerializeField] float shotCooldown;
    [SerializeField] public float shotPower;
    [SerializeField] float reloadTime;
    [SerializeField] bool automatic;
    [SerializeField] bool hitscan;

    [Header("Info")]
    [SerializeField] int shotCounter;
    [SerializeField] public bool shooting;

    Ray aimRay;
    public RaycastHit aimHit;
    bool aiming;


    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    /// <summary>
    /// Gets if the player is shooting (depends on if gun is automatic or not) or reloading, and call its respective functions
    /// </summary>
    void GetInput()
    {
        //Shooting
        if (Cursor.visible == false)
        {
            if (automatic)
            {
                if (Input.GetButton("Shoot"))
                {
                    if (shotCounter < capacity && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }

            if (!automatic)
            {
                if (Input.GetButtonDown("Shoot"))
                {
                    if (shotCounter < capacity && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }

            //reload
            if (Input.GetButtonDown("Reload"))
            {
                StartCoroutine(Reload());
            }
        }
    }

    /// <summary>
    /// Spawns bullet (at aim point if hitscan, at gun if not), reduces ammo, plays muzzle effect and puts gun on cooldown
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        //projectile bullet
        if (!hitscan)
        {
            GameObject bullet = bulletPool.transform.GetChild(0).gameObject;

            bullet.SetActive(true);
            bullet.transform.SetParent(null);
        }

        //hitscan bullet
        if (hitscan)
        {
            if (aiming && aimHit.collider.tag != "Aim Collider")
            {
                GameObject bullet = bulletPool.transform.GetChild(0).gameObject;

                bullet.SetActive(true);
                bullet.transform.position = aimHit.point;
                bullet.transform.parent = null;
            }
        }

        //manage ammo
        shooting = true;
        shotCounter++;

        //muzzle flash (quem diria em)
        muzzle.SetActive(true);
        muzzleFX.Play();

        //play animation
        animator.Play("Shoot", 1);        

        yield return new WaitForSeconds(shotCooldown);
        shooting = false;
    }

    /// <summary>
    /// Try guessing (it resets ammo counter)
    /// </summary>
    /// <returns></returns>
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        shotCounter = 0;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        aimColliders.SetActive(true);
        animator = player.GetComponent<Animator>();
        muzzleFX = muzzle.GetComponent<VisualEffect>();
    }

    void Update()
    {
        GetInput();

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        aiming = Physics.Raycast(aimRay, out aimHit);
    }

    void OnDisable()
    {
        muzzle.SetActive(false);
    }

    #endregion
    //========================


}

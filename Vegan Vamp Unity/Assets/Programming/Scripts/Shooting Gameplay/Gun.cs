using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Imports")]
    //game objects
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPool;
    [SerializeField] GameObject aimColliders;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject holsteredGun;
    [SerializeField] GameObject grapesObj;
    [SerializeField] GameObject gunBody;

    public List<GameObject> grapes = new List<GameObject>();

    //components
    MeshRenderer meshRenderer;
    Animator animator;
    VisualEffect muzzleFX;

    //scripts
    [SerializeField] Inventory inventory;
    Movement playerMovement;
    StatsManager playerStats;

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
    [SerializeField] public int shotCounter;
    [SerializeField] public bool shooting;
    
    float playerBaseSpeed;

    Ray aimRay;
    public RaycastHit aimHit;
    bool aiming;
    bool switchCooldown = false;


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
            if (playerStats.ice[StatsConst.SELF_INTENSITY] <= 0)
            {
                //shoot and reload if gun drawn
                if (meshRenderer.enabled)
                {
                    //shoot
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
                            //check if inventory aint open
                            if (!inventory.openMode)
                            {
                                //check ammo and shooting cooldown
                                if (shotCounter < capacity && !shooting)
                                {
                                    //shoot if gun drawn
                                    if (meshRenderer.enabled)
                                    {
                                        StartCoroutine(Shoot());
                                    }

                                    //draw gun if not
                                    else
                                    {
                                        //visuals
                                        meshRenderer.enabled = true;
                                        grapesObj.SetActive(true);
                                        holsteredGun.SetActive(false);

                                        //layer animations
                                        animator.SetLayerWeight(AnimationConsts.GUN_LAYER, 1);

                                        //slow down movement
                                        playerMovement.moveSpeed = playerBaseSpeed * 0.70f;
                                    }
                                }
                            }
                        }
                    }

                    //reload
                    if (Input.GetButtonDown("Reload") && shotCounter != 0)
                    {
                        StartCoroutine(Reload());
                    }
                }
            }

            //holster/withdraw
            if (Input.mouseScrollDelta.y != 0 && !switchCooldown)
            {
                if (meshRenderer.enabled == false)
                {
                    //visuals
                    meshRenderer.enabled = true;
                    grapesObj.SetActive(true);
                    holsteredGun.SetActive(false);

                    //layer animations
                    animator.SetLayerWeight(AnimationConsts.GUN_LAYER, 1);

                    //slow down movement
                    playerMovement.moveSpeed = playerBaseSpeed * 0.70f;

                    //switch cooldown
                    StartCoroutine(SwitchCooldown());
                }

                else
                {
                    //visuals
                    meshRenderer.enabled = false;
                    grapesObj.SetActive(false);
                    holsteredGun.SetActive(true);

                    //unlayer animations
                    animator.SetLayerWeight(AnimationConsts.GUN_LAYER, 0);

                    //set movement to normal speed
                    playerMovement.moveSpeed = playerBaseSpeed;

                    //switch cooldown
                    StartCoroutine(SwitchCooldown());
                }
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

        //remove grapes
        if (gameObject.name == "Grape Shooter")
        {
            grapes[shotCounter].SetActive(false);
        }

        //manage ammo
        shooting = true;
        shotCounter++;

        //muzzle flash (quem diria em)
        muzzle.SetActive(true);
        muzzleFX.Play();

        //play animation
        animator.Play("Shoot", AnimationConsts.GUN_LAYER);        

        yield return new WaitForSeconds(shotCooldown);
        shooting = false;
    }

    /// <summary>
    /// Try guessing (it resets ammo counter)
    /// </summary>
    /// <returns></returns>
    IEnumerator Reload()
    {
        while (shotCounter != 0)
        {
            //bring grapes back
            if (gameObject.name == "Grape Shooter")
            {
                grapes[shotCounter - 1].SetActive(true);
            }

            yield return new WaitForSeconds(reloadTime/capacity);
            shotCounter -= 1;
        }
    }

    IEnumerator SwitchCooldown()
    {
        switchCooldown = true;
        yield return new WaitForSeconds(0.35f);
        switchCooldown = false;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get game objects
        foreach (Transform grape in grapesObj.transform)
        {
            grapes.Add(grape.gameObject);
        }

        //get components
        aimColliders.SetActive(true);
        animator = player.GetComponent<Animator>();
        muzzleFX = muzzle.GetComponent<VisualEffect>();
        meshRenderer = gunBody.GetComponent<MeshRenderer>();

        //get scripts
        playerMovement = player.GetComponent<Movement>();
        playerStats = player.GetComponent<StatsManager>();

        //get values
        playerBaseSpeed = playerMovement.moveSpeed;
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

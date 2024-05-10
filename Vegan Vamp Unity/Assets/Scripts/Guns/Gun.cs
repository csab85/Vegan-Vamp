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
    [SerializeField] GameObject bulletPool;
<<<<<<< HEAD
    [SerializeField] GameObject aimColliders;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject hitPool;
    VisualEffect muzzleFX;
=======
    GameObject bullet;
>>>>>>> Scenery

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
    [SerializeField] bool shooting;

    Ray aimRay;
    public RaycastHit aimHit;
    Vector3 hitPoint;
<<<<<<< HEAD
    bool aiming;
=======

    [SerializeField] GameObject coiso;


>>>>>>> Scenery


    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

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

    IEnumerator Shoot()
    {
        //bullet managing
<<<<<<< HEAD
        if (!hitscan)
        {
            GameObject bullet = bulletPool.transform.GetChild(0).gameObject;

            bullet.SetActive(true);
            bullet.transform.SetParent(null);
=======
        if (shotCounter <= capacity)
        {
            bullet = bulletPool.transform.GetChild(0).gameObject;

            bullet.SetActive(true);
            bullet.transform.SetParent(null);
            bullet.GetComponent<BaseBullet>().movingToTarget = true;

            shooting = true;
            shotCounter++;

            yield return new WaitForSecondsRealtime(shotCooldown);
            shooting = false;

            Instantiate(coiso);
>>>>>>> Scenery
        }

        //muzzle (quem diria em)
        muzzleFX.Play();

        //hit (only for hitscan)
        if (hitscan)
        {
            if (aiming && aimHit.collider.tag != "Aim Collider")
            {
                GameObject bullet = bulletPool.transform.GetChild(0).gameObject;

                bullet.SetActive(true);
                bullet.transform.position = aimHit.point;
            }
        }

        //manage ammo
        shooting = true;
        shotCounter++;

        yield return new WaitForSecondsRealtime(shotCooldown);
        shooting = false;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(reloadTime);
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
        muzzleFX = muzzle.GetComponent<VisualEffect>();
    }

    void Update()
    {
        GetInput();

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
<<<<<<< HEAD
        aiming = Physics.Raycast(aimRay, out aimHit);
=======
        Physics.Raycast(aimRay, out aimHit);
        hitPoint = Camera.main.ScreenToWorldPoint(aimHit.point);
        


>>>>>>> Scenery
    }

    #endregion
    //========================


}

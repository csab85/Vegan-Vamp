using System;
using System.Collections;
using UnityEngine;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Imports")]
    [SerializeField] GameObject bulletPool;
    [SerializeField] GameObject aimColliders;
    [SerializeField] GameObject muzzleFX;
    [SerializeField] GameObject hitFX;
    GameObject bullet;

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
        if (!hitscan)
        {
            bullet = bulletPool.transform.GetChild(0).gameObject;

            bullet.SetActive(true);
            bullet.transform.SetParent(null);
            bullet.GetComponent<BaseBullet>().movingToTarget = true;
        }

        if (hitscan)
        {

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
    }

    void Update()
    {
        GetInput();

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
    }

    #endregion
    //========================


}

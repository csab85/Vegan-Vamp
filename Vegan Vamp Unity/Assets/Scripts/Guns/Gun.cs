using System;
using System.Collections;
using UnityEngine;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Game Objects")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletPool;

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


    public int shotCounter = 0;
    public bool shooting = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator Shoot()
    {
        //bullet managing
        int bulletNum = shotCounter;

        while (bulletNum > bulletPool.transform.childCount)
        {
            bulletNum =- bulletPool.transform.childCount;
        }

        bullet = bulletPool.transform.GetChild(bulletNum).gameObject;

        bullet.SetActive(true);

        shooting = true;

        bullet.transform.SetParent(null);
        
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

    void Update()
    {
        //Shooting
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

    #endregion
    //========================


}

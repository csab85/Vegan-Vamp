using System;
using System.Collections;
using UnityEngine;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Imports")]
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

    [Header("Info")]
    [SerializeField] int shotCounter;
    [SerializeField] bool shooting;

[SerializeField] Transform coiso;
    Ray aimRay;
    public RaycastHit aimHit;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator Shoot()
    {
        //bullet managing
        

        //while (bulletNum > bulletPool.transform.childCount)
        //{
        //    bulletNum =- bulletPool.transform.childCount;
        //}
        if (shotCounter <= capacity)
        {
            int bulletNum = shotCounter;

            bullet = bulletPool.transform.GetChild(bulletNum).gameObject;

            bullet.SetActive(true);
            bullet.transform.SetParent(null);
            bullet.GetComponent<BaseBullet>().movingToTarget = true;

            shooting = true;
            shotCounter++;

            yield return new WaitForSecondsRealtime(shotCooldown);
            shooting = false;
        }
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
        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
        coiso.position = aimHit.point;

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

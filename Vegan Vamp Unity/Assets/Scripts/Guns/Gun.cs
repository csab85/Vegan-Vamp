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

    [Header("Info")]
    [SerializeField] int shotCounter;
    [SerializeField] bool shooting;

    Ray aimRay;
    public RaycastHit aimHit;
    Vector3 hitPoint;

    [SerializeField] GameObject coiso;




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
        GetInput();

        //Aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, Screen.height / 2);
        aimRay = Camera.main.ScreenPointToRay(screenAim);
        Physics.Raycast(aimRay, out aimHit);
        hitPoint = Camera.main.ScreenToWorldPoint(aimHit.point);
        


    }

    #endregion
    //========================


}

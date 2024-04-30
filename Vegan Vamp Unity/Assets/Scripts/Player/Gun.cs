using System;
using UnityEngine;

public class Gun: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    [Header("Game Objects")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject[] storedBullets;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject crosshair;

    [Header("Settings")]
    [SerializeField] bool automatic;
    [SerializeField] float shotPower;

    int shotCounter = 0;



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Shoot()
    {
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody>().isKinematic = false;
        bullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * shotPower, ForceMode.VelocityChange);


        //storedBullets[shotCounter].SetActive(false);
        shotCounter++;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        //aim
        Vector2 screenAim = new Vector2 (Screen.width / 2, (Screen.height / 3 * 2));
        Ray aimRay = Camera.main.ScreenPointToRay(screenAim);

        if (Physics.Raycast(aimRay, out RaycastHit hitInfo))
        {
            crosshair.transform.position = hitInfo.point;
        }

        if (automatic)
        {
            
        }

        if (!automatic)
        {
            if (Input.GetButtonDown("Shoot"))
            {
                Shoot();
            }
        }
    }

    #endregion
    //========================


}

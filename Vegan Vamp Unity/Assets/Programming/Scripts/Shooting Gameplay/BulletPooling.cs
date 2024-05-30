using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject bullet;
    [SerializeField] int bulletsNum;

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

    void PoolBullets()
    {
        for (int i = 0; i < bulletsNum; i++)
        {
            GameObject bulletChild = Instantiate(bullet, Vector3.zero, Quaternion.identity);
            bulletChild.transform.parent = transform;
            bulletChild.transform.name = "Bullet " + (i + 1);
            bulletChild.transform.localPosition = Vector3.zero;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        PoolBullets();
    }

    #endregion
    //========================


}

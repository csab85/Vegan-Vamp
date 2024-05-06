using UnityEngine;

public class GunSelector : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject[] guns;

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

    public void SelectGun(int gunNumber)
    {
        foreach (GameObject gun in guns)
        {
            if (gun == guns[gunNumber])
            {
                gun.SetActive(true);
            }

            else
            {
                gun.SetActive(false);
            }
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

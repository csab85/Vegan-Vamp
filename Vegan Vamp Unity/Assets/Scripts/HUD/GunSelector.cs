using UnityEngine;

public class GunSelector : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject[] guns;
    [SerializeField] ThirdPersonCamera camScript;

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

    /// <summary>
    /// Iterates list, activates the selected gun and deactivates the others
    /// </summary>
    /// <param name="gunNumber">The gun to be activated</param>
    public void SelectGun(int gunNumber)
    {
        if (camScript.currentMode == ThirdPersonCamera.CameraMode.Combat)
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
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        if (camScript.currentMode == ThirdPersonCamera.CameraMode.Exploration)
        {
            foreach(GameObject gun in guns)
            {
                if (gun.activeSelf)
                {
                    gun.SetActive(false);
                }    
            }
        }
    }

    #endregion
    //========================


}

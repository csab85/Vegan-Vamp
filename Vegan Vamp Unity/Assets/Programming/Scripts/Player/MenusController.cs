using UnityEngine;

public class MenusController : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject weaponWheel;

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

    void GetInput()
    {
        if (Input.GetButton("Weapon Wheel") && !weaponWheel.activeSelf)
        {
            weaponWheel.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (!Input.GetButton("Weapon Wheel") && weaponWheel.activeSelf)
        {
            weaponWheel.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        GetInput();
    }

    #endregion
    //========================


}

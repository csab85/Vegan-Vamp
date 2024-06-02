using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] guns;
    [SerializeField] ThirdPersonCamera camScript;

    GameObject weaponWheelUI;
    Animator animator;
    StatsManager playerStats;

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

    void Start()
    {
        weaponWheelUI = transform.GetChild(0).gameObject;
        animator = player.GetComponent<Animator>();
        playerStats = player.GetComponent<StatsManager>();
    }

    void Update()
    {
        //open wheel if on combat mode
        if(camScript.currentMode == ThirdPersonCamera.CameraMode.Combat)
        {
            if (Input.GetButton("Weapon Wheel") && !weaponWheelUI.activeSelf && !playerStats.dead)
            {
                weaponWheelUI.SetActive(true);

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            if (!Input.GetButton("Weapon Wheel") && weaponWheelUI.activeSelf)
            {
                weaponWheelUI.SetActive(false);

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            animator.SetLayerWeight(AnimationConsts.GUN_LAYER, 1);
        }


        //deactivate all guns if on exploration mode
        if (camScript.currentMode == ThirdPersonCamera.CameraMode.Exploration)
        {
            foreach(GameObject gun in guns)
            {
                if (gun.activeSelf)
                {
                    gun.SetActive(false);
                }    
            }

            animator.SetLayerWeight(AnimationConsts.GUN_LAYER, 0);
        }
    }

    #endregion
    //========================


}

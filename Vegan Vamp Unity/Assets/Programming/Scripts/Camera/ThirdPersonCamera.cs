using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] Transform playerTransf;
    [SerializeField] Transform orientTransf;
    [SerializeField] Transform combatLookAt;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject crosshair;
    [SerializeField] Inventory inventory;

    [Header ("Cameras")]
    [SerializeField] GameObject explorationCamera;
    [SerializeField] GameObject combatCamera;

    StatsManager playerStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float rotationSpeed;
    [SerializeField] public CameraMode currentMode;

    int CameraModeNumber = 0;

    public enum CameraMode
    {
        Exploration,
        Combat
    }

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void SwitchCameraMode(int modeNumber)
    {
        CameraMode[] modes = {CameraMode.Exploration, CameraMode.Combat};

        explorationCamera.SetActive(false);
        combatCamera.SetActive(false);

        if(modes[modeNumber] == CameraMode.Exploration)
        {
            explorationCamera.SetActive(true);
            crosshair.SetActive(false);
        }

        if(modes[modeNumber] == CameraMode.Combat)
        {
            combatCamera.SetActive(true);
            crosshair.SetActive(true);
        }

        currentMode = modes[modeNumber];
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerStats = playerTransf.gameObject.GetComponent<StatsManager>();
    }

    private void FixedUpdate()
    {
        if (!playerStats.dead)
        {
            //rotate orientation
            Vector3 viewDirection = playerTransf.position - new Vector3(transform.position.x, playerTransf.position.y , transform.position.z);

            orientTransf.forward = viewDirection.normalized;

            //rotate player
            if (currentMode == CameraMode.Exploration)
            {
                float horizontalInput = Input.GetAxis ("Horizontal");
                float verticalInput = Input.GetAxis ("Vertical");

                Vector3 inputDirection = orientTransf.forward * verticalInput + orientTransf.right * horizontalInput;

                if (inputDirection != Vector3.zero)
                {
                    //THIS WILL POSSIBLY NEED DELTA TIME
                    playerTransf.forward = Vector3.Lerp(playerTransf.forward, inputDirection.normalized, rotationSpeed * Time.deltaTime);
                }
            }

            //fix player look forward
            else if (currentMode == CameraMode.Combat)
            {
                Vector3 combatViewDirection = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y , transform.position.z);

                playerTransf.forward = combatViewDirection.normalized;
            }

            //Switch camera
            if (Input.GetButtonDown("Switch Camera"))
            {
                CameraModeNumber = (CameraModeNumber + 1) % 2;

                SwitchCameraMode(CameraModeNumber);
            }

            //Stop movement while on inventory
            if (inventory.openMode)
            {
                explorationCamera.SetActive(false);
            }

            else if (currentMode == CameraMode.Exploration && !explorationCamera.activeSelf)
            {
                explorationCamera.SetActive(true);
            }
        }
    }

    #endregion
    //========================


}

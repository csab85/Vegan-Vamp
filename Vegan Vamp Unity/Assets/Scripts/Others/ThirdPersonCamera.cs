using Unity.VisualScripting;
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

    [Header ("Cameras")]
    [SerializeField] GameObject explorationCamera;
    [SerializeField] GameObject combatCamera;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float rotationSpeed;
    [SerializeField] cameraMode currentMode;

    public enum cameraMode
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
        cameraMode[] modes = {cameraMode.Exploration, cameraMode.Combat};

        explorationCamera.SetActive(false);
        combatCamera.SetActive(false);

        if(modes[modeNumber] == cameraMode.Exploration)
        {
            explorationCamera.SetActive(true);
            crosshair.SetActive(false);
        }

        if(modes[modeNumber] == cameraMode.Combat)
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
    }

    private void Update()
    {
        //rotate orientation
        Vector3 viewDirection = playerTransf.position - new Vector3(transform.position.x, playerTransf.position.y , transform.position.z);

        orientTransf.forward = viewDirection.normalized;

        //rotate player
        if (currentMode == cameraMode.Exploration)
        {
            float horizontalInput = Input.GetAxis ("Horizontal");
            float verticalInput = Input.GetAxis ("Vertical");

            Vector3 inputDirection = orientTransf.forward * verticalInput + orientTransf.right * horizontalInput;

            if (inputDirection != Vector3.zero)
            {
                playerTransf.forward = Vector3.Slerp(playerTransf.forward, inputDirection.normalized, rotationSpeed * Time.deltaTime);
            }
        }

        else if (currentMode == cameraMode.Combat)
        {
            Vector3 combatViewDirection = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y , transform.position.z);

            playerTransf.forward = combatViewDirection.normalized;
        }
    }

    #endregion
    //========================


}

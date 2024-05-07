using Unity.VisualScripting;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Scripts")]
    [SerializeField] Movement movementScript;
    [SerializeField] ThirdPersonCamera cameraScript;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    float horizontalInput;
    float verticalInput;

    //camera mode
    int cameraModeNumber = 0;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            movementScript.Jump();
        }

        if (Input.GetButtonDown("Switch Camera"))
        {
            cameraModeNumber ++;

            if (cameraModeNumber > 1)
            {
                cameraModeNumber = 0;
            }

            cameraScript.SwitchCameraMode(cameraModeNumber);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void FixedUpdate()
    {
        movementScript.MovePlayer(verticalInput, horizontalInput);
    }

    void Update()
    {
        GetInputs();
    }

    #endregion
    //========================


}

using Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    //game objects
    GameObject player;
    [SerializeField] GameObject crosshair;

    //components
    [SerializeField] Transform playerTransf;
    [SerializeField] Transform orientTransf;
    [SerializeField] Transform combatLookAt;
    [SerializeField] Rigidbody playerRb;
    [HideInInspector] public CinemachineFreeLook combatCinemachine;
    [HideInInspector] public CinemachineFreeLook explorationCinemachine;
    Animator animator;

    //scripts
    [SerializeField] Inventory inventory;
    StatsManager playerStats;
    Movement playerMovement;

    [Header ("Cameras")]
    //game objects
    [SerializeField] public GameObject explorationCamera;
    [SerializeField] public GameObject combatCamera;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float rotationSpeed;

    float playerBaseSpeed;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void SwitchCameraMode()
    {
        if (explorationCinemachine.Priority > combatCinemachine.Priority)
        {
            explorationCinemachine.Priority = 0;
            combatCinemachine.Priority = 1;
        }

        else
        {
            explorationCinemachine.Priority = 1;
            combatCinemachine.Priority = 0;
        }
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

        //get game objects
        player = playerTransf.gameObject;

        //get components
        animator = player.GetComponent<Animator>();
        combatCinemachine = combatCamera.GetComponent<CinemachineFreeLook>();
        explorationCinemachine = explorationCamera.GetComponent<CinemachineFreeLook>();

         //get scripts
        playerStats = player.GetComponent<StatsManager>();
        playerMovement = player.GetComponent<Movement>();

        playerBaseSpeed = playerMovement.moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!playerStats.dead)
        {
            //rotate orientation
            Vector3 viewDirection = playerTransf.position - new Vector3(transform.position.x, playerTransf.position.y , transform.position.z);

            orientTransf.forward = viewDirection.normalized;

            //rotate player
            if (explorationCinemachine.Priority > combatCinemachine.Priority)
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
            else if (explorationCinemachine.Priority < combatCinemachine.Priority)
            {
                Vector3 combatViewDirection = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y , transform.position.z);

                playerTransf.forward = combatViewDirection.normalized;
            }

            //Select right camera mode
            if (animator.GetLayerWeight(AnimationConsts.GUN_LAYER) == 1 | animator.GetLayerWeight(AnimationConsts.BOTTLE_LAYER) == 1)
            {
                if (combatCinemachine.Priority < explorationCinemachine.Priority)
                {
                    crosshair.SetActive(true);
                    combatCinemachine.Priority = 1;
                    explorationCinemachine.Priority = 0;
                }
            }

            else
            {
                if (combatCinemachine.Priority > explorationCinemachine.Priority)
                {
                    crosshair.SetActive(false);
                    combatCinemachine.Priority = 0;
                    explorationCinemachine.Priority = 1;
                }
            }

            //Stop movement while on inventory
            if (inventory.openMode)
            {
                playerMovement.moveSpeed = 0;
            }

            else if (playerMovement.moveSpeed != playerBaseSpeed)
            {
                playerMovement.moveSpeed = playerBaseSpeed;
            }
        }
    }

    #endregion
    //========================


}

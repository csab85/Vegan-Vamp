using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerPivot;

    //player settings
    [Header("Player Settings")][Tooltip("Settings the player can adjust")]
    public Vector2 rotationSpeed;
    public float camShoulderOffset;

    //game settings
    [Header("Game Settings")][Tooltip("Settings the player can't adjust")]
    [SerializeField] float camDistance;
    [SerializeField] float camHeight;
    [SerializeField][Tooltip("Min and max camera angle on axis X (looking up and down)")] Vector2 minMaxPitch;


    Vector3 camOffset;

    //camera rotation

    Vector3 rotation;
    Vector3 mouseDelta;

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

    

    #endregion
    //========================


    //RUNNING
    //========================
    #region
    void Update()
    {
        //position pivot
        playerPivot.transform.localPosition = new Vector3 (camShoulderOffset, 0, 0);

        camOffset = new Vector3(0, camHeight, -camDistance);

        //rotate camera
        //get mouse movement
        mouseDelta.x = -Input.GetAxis("Mouse Y");
        mouseDelta.y = Input.GetAxis("Mouse X");

        //limit camera movement
        rotation.x = Mathf.Clamp(rotation.x, minMaxPitch.x, minMaxPitch.y);

        //add movement to rotation vector
        rotation.x += mouseDelta.x * rotationSpeed.x  * 100 * Time.deltaTime;
        rotation.y += mouseDelta.y * rotationSpeed.y  * 100 * Time.deltaTime;

        //update offset
        camOffset  = Quaternion.Euler(rotation) * camOffset;
        

        //update transform values
        transform.position = playerPivot.transform.position + camOffset;
        transform.LookAt(playerPivot.transform);
        
        //make player face same direction
        player.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //center mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion
    //========================


}

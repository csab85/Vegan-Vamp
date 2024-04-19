using Unity.Mathematics;
using UnityEditor.EditorTools;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;

    //player settings
    [Header("Player Settings")][Tooltip("Settings the player can adjust")]
    public Vector2 rotationSpeed;
    public float cameraShoulderOffset;

    //game settings
    [Header("Game Settings")][Tooltip("Settings the player can't adjust")]
    [SerializeField] float cameraDistance;
    [SerializeField] float cameraHeight;
    Vector3 cameraOffset;

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
        //position camera
        cameraOffset = new Vector3(-cameraShoulderOffset, -cameraHeight, cameraDistance);

        //rotate camera
        mouseDelta.x = -Input.GetAxis("Mouse Y");
        mouseDelta.y = Input.GetAxis("Mouse X");

        rotation.x = Mathf.Clamp(rotation.x, -90, 90);
        rotation.y = Mathf.Clamp(rotation.y, -60, 60);

        rotation += Vector3.Scale(mouseDelta, rotationSpeed) * Time.deltaTime;
        

        //update transform values
        transform.position = player.transform.position - cameraOffset;
        transform.rotation = Quaternion.Euler(rotation);
    }

    #endregion
    //========================


}

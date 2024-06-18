using UnityEngine;

public class Minimap: MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Transform player;

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

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    #endregion
    //========================


}

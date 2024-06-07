using UnityEngine;

public class ScaredBug : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject speedIngredient;

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

    public void SpawnIngredient()
    {
        Instantiate(speedIngredient, transform.position, Quaternion.identity, null);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

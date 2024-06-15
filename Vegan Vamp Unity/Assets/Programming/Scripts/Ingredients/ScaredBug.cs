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
        GameObject newIngredient = Instantiate(speedIngredient, transform.position, Quaternion.identity, null);
        newIngredient.name = "Speed Ingredient";
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BlenderJuice : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Material juiceMaterial;

    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            StatsManager ingredientStats = collision.gameObject.GetComponent<StatsManager>();

            foreach (var item in ingredientStats.statsDict)
            {
                StatsManager.Stats stat = item.Key;

                float ingredientApplyIntensity = ingredientStats.statsDict[stat][APPLY_INTENSITY];
                float ingredientApplyDuration = ingredientStats.statsDict[stat][APPLY_DURATION]; 

                selfStats.AddToSelfApply(stat, ingredientApplyIntensity, ingredientApplyDuration);
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
        selfStats = GetComponent<StatsManager>();
    }

    #endregion
    //========================


}

using UnityEngine;

public class FireFruit : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    StatsManager selfStats;


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

    void Start()
    {
        selfStats = GetComponent<StatsManager>();
    }

    void Update()
    {
        if (selfStats.fire[StatsConst.SELF_INTENSITY] < 0.1f)
        {
            selfStats.fire[StatsConst.DEFAULT_BASE] = 0;
        }

        else if (selfStats.fire[StatsConst.SELF_INTENSITY] > 0.1f)
        {
            selfStats.fire[StatsConst.DEFAULT_BASE] = 0.8f;
        }
    }

    #endregion
    //========================


}

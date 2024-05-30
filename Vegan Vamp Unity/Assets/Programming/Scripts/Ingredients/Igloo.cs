using UnityEngine;

public class Igloo : MonoBehaviour
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

    float iceDivider; //use this to to normalize scale value based on health

    const int DEFAULT_BASE = 0;
    const int CURRENT_BASE = 1;
    const int SELF_INTENSITY = 2;
    const int SELF_REACH_TIME = 3;
    const int SELF_RETURN_TIME = 4;
    const int APPLY_INTENSITY = 5;
    const int APPLY_REACH_TIME = 6;
    const int APPLY_RETURN_TIME = 7;
    const int CAP_INTENSITY = 8;
    const int CAP_REACH_TIME = 9;
    const int CAP_RETURN_TIME = 10;
    const int STARTING_BASE = 11;
    const int STARTING_INTENSITY = 12;
    const int PASSED_TIME = 13;

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
        iceDivider = selfStats.ice[SELF_INTENSITY];
    }

    void Update()
    {
        if (selfStats.ice[SELF_INTENSITY] > 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x, selfStats.ice[SELF_INTENSITY], transform.localScale.z);
        }

        if (selfStats.ice[SELF_INTENSITY] <= 0.1f)
        {
            foreach (Transform ingredient in transform.parent)
            {
                if (ingredient.name == "Ice Ingredient")
                {
                    ingredient.gameObject.GetComponent<SphereCollider>().enabled = true;
                }
            }

            //zero health stat
            selfStats.ApplyToBase(0, 0, 0);

            selfStats.ice[DEFAULT_BASE] = 0.1f;
        }
    }

    #endregion
    //========================


}

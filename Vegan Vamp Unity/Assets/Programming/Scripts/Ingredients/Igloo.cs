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
        iceDivider = selfStats.ice[StatsConst.SELF_INTENSITY];
    }

    void Update()
    {
        if (selfStats.ice[StatsConst.SELF_INTENSITY] > 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x, selfStats.ice[StatsConst.SELF_INTENSITY], transform.localScale.z);
        }

        if (selfStats.ice[StatsConst.SELF_INTENSITY] <= 0.1f)
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

            selfStats.ice[StatsConst.DEFAULT_BASE] = 0.1f;
        }
    }

    #endregion
    //========================


}

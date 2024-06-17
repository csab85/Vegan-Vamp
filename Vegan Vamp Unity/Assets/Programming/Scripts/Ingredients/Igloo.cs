using UnityEngine;

public class Igloo : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Transform[] iceFlowers;

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

    void OnEnable()
    {
        //grow ice again
        selfStats.ice[StatsConst.DEFAULT_BASE] = 1;
    }

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

            foreach (Transform ingredient in iceFlowers)
            {
                if (ingredient.tag != "Untagged")
                {
                    ingredient.tag = "Untagged";
                    ingredient.GetComponent<Collider>().enabled = false;
                }
            }
        }

        if (selfStats.ice[StatsConst.SELF_INTENSITY] <= 0.1f)
        {
            //keep ice melt
            selfStats.ice[StatsConst.DEFAULT_BASE] = 0.01f;

            //make flowers harvestable
            if (selfStats.fire[StatsConst.SELF_INTENSITY] <= 0)
            {
                foreach (Transform ingredient in iceFlowers)
                {
                    if (ingredient.tag != "Ingredient")
                    {
                        ingredient.tag = "Ingredient";
                    }
                }
            }            
        }
    }

    #endregion
    //========================


}

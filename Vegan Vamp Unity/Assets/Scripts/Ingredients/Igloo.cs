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
        transform.localScale = new Vector3(transform.localScale.x, selfStats.ice[SELF_INTENSITY] / iceDivider, transform.localScale.z);

        if (selfStats.ice[SELF_INTENSITY] <= 0)
        {
            foreach (Transform ingredient in transform.parent)
            {
                if (ingredient.name == "Ice Ingridient")
                {
                    ingredient.gameObject.GetComponent<SphereCollider>().enabled = true;
                }
            }

            //zero health stat
            selfStats.ApplyToBase(0, 0, 0);

            Destroy(gameObject);
        }
    }

    #endregion
    //========================


}

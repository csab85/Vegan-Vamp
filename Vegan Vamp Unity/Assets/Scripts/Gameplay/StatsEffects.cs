using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

//VFX container gotta be the third object

public class StatsEffects : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    StatsManager statsManager;

    //FX
    GameObject fire;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    //Consts
    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

    //Stats order
    const int FIRE = 0;

    //Import enum
    StatsManager.Stats Burning = StatsManager.Stats.Burning;

    //create dict
    Dictionary<StatsManager.Stats, float[]> statsDict = new Dictionary<StatsManager.Stats, float[]>();

    //Check what coroutine is running
    bool burning;

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
        statsManager = GetComponent<StatsManager>();

        //Get vfx objects
        Transform VFX = transform.GetChild(2);

        fire = VFX.GetChild(FIRE).gameObject;
    }

    void Update()
    {
        //update dict values
        statsDict = statsManager.statsDict;

        //FIRE
        if (statsDict[Burning][SELF_INTENSITY] > 0)
        {
            if (!fire.activeSelf)
            {
                fire.SetActive(true);
            }

            float scale = statsDict[Burning][SELF_INTENSITY];

            fire.transform.localScale = new Vector3(scale, scale, scale);
        }

        else
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
            }
        }
    }

    #endregion
    //========================


}

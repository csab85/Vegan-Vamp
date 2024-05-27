using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //EXPLANATION
    //Every StatsManager has a dict, and each dict stores each stat as a key (the stats can be found in the enum Stats). The value is an array of 8 floats, that refers to aspects of each stat. Those aspects are  always the same, but with different values.


    //DEFAULT BASE (0) - The value the current base will drift towards (current base only drifts towards if self intensity = current base

    //CURRENT BASE (1) - The value the intensity will drift towards

    //SELF INTENSITY (2) - How intense the effect is on the object

    //SELF REACH TIME (3) - How many seconds it'll take for the intensity to get to current base. If <= 0 the value won't change

    //SELF RETURN TIME (4) - How many seconds it'll take for current base to reach default base

    //APPLY INTENSITY (5) - How much of this effect will be added to the targets SELF INTENSITY. If negative reduces the intensity

    //APPLY REACH TIME (6) - How much seconds more it'll take to SELF INTENSITY to go to the same as CURRENT BASE

    //APPLY RETURN TIME (7) - How much seconds more it'll take to CURRENT BASE to go to the same as DEFAULT BASE

    //CAP INTENSITY (8) - The highest SELF INTENSITY the object can get to

    //CAP REACH TIME (9) - The highest SELF REACH TIME the instance can have

    //CAP RETURN TIME (10) - The highest SELF RETURN TIME the instance can have

    //CAP STARTING INTENSITY (11) - How much intensity the object had the when SELF INTENSITY changes to a new value different from BASE INTENSITY

    //PASSED TIME (12) - How many seconds passed since last time SELF INTENSITY != BASE INTENSITY


    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    //the value to call each var inside a stat list
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
    
    //Stats list of the game object

    [Tooltip ("")]
    [SerializeField] public float[] health;
    [SerializeField] public float[] fire;
    [SerializeField] public float[] ice;
    


    //Create jagged array
    public float[][] statsArray;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    //se der errado usar o nome ao inves de float
    /// <summary>
    /// Applies the a stat on the object calling this function
    /// </summary>
    /// <param name="stat">The stat being applied</param>
    /// <param name="intensity">How much intensity is being added</param>
    /// <param name="reachTime">How much time to reach is being added</param>
    /// /// <param name="returnTime">How much time to return is being added</param>
    public void ApplyStatSelf(int statNum, float intensity, float reachTime, float returnTime)
    {
        statsArray[statNum][CURRENT_BASE] += intensity;

        statsArray[statNum][SELF_REACH_TIME] += reachTime;
        statsArray[statNum][STARTING_INTENSITY] = statsArray[statNum][SELF_INTENSITY];

        statsArray[statNum][SELF_RETURN_TIME] += returnTime + reachTime;
        statsArray[statNum][STARTING_BASE] = statsArray[statNum][STARTING_BASE];
    }

    public void ApplyToBase(int statNum, float intensity, float returnTime = -1)
    {
        statsArray[statNum][DEFAULT_BASE] = intensity;

        if (returnTime >= 0)
        {
            statsArray[statNum][SELF_RETURN_TIME] = returnTime + statsArray[statNum][SELF_REACH_TIME];
        }
    }

    public void AddToSelfApply(int statNum, float intensity, float reachTime, float returnTime)
    {
        statsArray[statNum][APPLY_INTENSITY] += intensity;
        statsArray[statNum][APPLY_REACH_TIME] += reachTime;
        statsArray[statNum][APPLY_RETURN_TIME] += returnTime;
    } 

    void DriftTowardsBase()
    {
        foreach (float[] stat in statsArray)
        {
            float defaultBase = stat[DEFAULT_BASE];
            float currentBase = stat[CURRENT_BASE];
            float selfIntensity = stat[SELF_INTENSITY];

            //move self intensity towards current base, based on how much of the reach time
            if (selfIntensity != currentBase)
            {
                float reachTime = stat[SELF_REACH_TIME];
                float passedTime = stat[PASSED_TIME];
                float startingIntensity = stat[STARTING_INTENSITY];

                //get the percentage of durtation passed based on time passed
                float reachTimePercentage = Mathf.Clamp01(passedTime / reachTime);
                
                //set self intensity to percentage between starting and base value (percentage of time passed from toal duration) 
                selfIntensity = Mathf.Lerp(startingIntensity, currentBase, reachTimePercentage);

                //Update values on dict
                stat[SELF_INTENSITY] = selfIntensity;
                stat[PASSED_TIME] += Time.deltaTime;
            }


            //move current base towards default base, based on how much of the return time
            if (currentBase != defaultBase && selfIntensity == currentBase)
            {
                float returnTime = stat[SELF_RETURN_TIME];
                float passedTime = stat[PASSED_TIME];
                float startingBase = stat[STARTING_BASE];

                //get the percentage of durtation passed based on time passed
                float returnTimePercentage = Mathf.Clamp01((passedTime) / returnTime);

                //set self intensity to percentage between starting and base value (percentage of time passed from toal duration) 
                currentBase = Mathf.Lerp(startingBase, defaultBase, returnTimePercentage);

                //Update values on dict and keep intensity equal to current base (this might makeintensity unchangeble)
                stat[CURRENT_BASE] = currentBase;
                stat[SELF_INTENSITY] = currentBase;
                stat[PASSED_TIME] += Time.deltaTime;
            }

            //passed time to 0 if base and self intensity are the same
            else if (stat[PASSED_TIME] != 0 | stat[SELF_REACH_TIME] != 0 | stat[SELF_RETURN_TIME] != 0)
            {
                stat[PASSED_TIME] = 0;
                stat[SELF_REACH_TIME] = 0;
                stat[SELF_RETURN_TIME] = 0;
            }
        }
    }

    //fix this
    //void CapValues()
    //{
    //    foreach (float[] stat in statsArray)
    //    {
    //        if (stat[SELF_INTENSITY] > stat[CAP_INTENSITY] && stat[CAP_INTENSITY] >= 0)
    //        {
    //            print(stat);
    //            stat[SELF_INTENSITY] = stat[CAP_INTENSITY];
    //        }

    //        if (stat[SELF_DURATION] > stat[CAP_DURATION] && stat[CAP_DURATION] >= 0)
    //        {
    //            stat[SELF_DURATION] = stat[CAP_DURATION];
    //        }

    //        if (stat[SELF_INTENSITY] < 0)
    //        {
    //            stat[SELF_INTENSITY] = 0;
    //        }

    //        if (stat[DEFAULT_BASE] < 0)
    //        {
    //            stat[DEFAULT_BASE] = 0;
    //        }
    //    }
    //}

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Awake()
    {
        if (statsArray == null)
        {
            statsArray = new float[][] {health, fire, ice};
        }   
    }

    void Update()
    {
        //CapValues();
        DriftTowardsBase();
    }

    #endregion
    //========================


}

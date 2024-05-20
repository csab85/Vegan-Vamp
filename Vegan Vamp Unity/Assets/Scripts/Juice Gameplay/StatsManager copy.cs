// using System.Collections.Generic;
// using UnityEngine;

// public class StatsManagerCopy  : MonoBehaviour
// {
//     //EXPLANATION
//     //Every StatsManager has a dict, and each dict stores each stat as a key (the stats can be found in the enum Stats). The value is an array of 8 floats, that refers to aspects of each stat. Those aspects are  always the same, but with different values.


//     //BASE INTENSITY (0) - The self intensity value the object will drift towards (if self intensity is different from base intensity)

//     //SELF INTENSITY (1) - How intense the effect is on the object

//     //SELF DURATION (2) - How many seconds it'll take for the intensity to get to it's base value. If <= 0 the value won't change

//     //APPLY INTENSITY (3) - How much of this effect will be added to the targets SELF INTENSITY. If negative reduces the intensity

//     //APPLY DURATION (4) - How much seconds more it'll take to SELF INTENSITY to go to the same as BASE INTENSITY
    
//     //CAP INTENSITY (5) - The highest SELF INTENSITY the object can get to

//     //CAP INTENSITY (6) - The highest SELF DURATION the object can get to

//     //CAP STARTING INTENSITY (7) - How much intensity the object had the when SELF INTENSITY changes to a new value different from BASE INTENSITY

//     //PASSED TIME (8) - How many seconds passed since last time SELF INTENSITY != BASE INTENSITY

//     //IMPORTS
//     //========================
//     #region



//     #endregion
//     //========================


//     //STATS AND VALUES
//     //========================
//     #region
//     //the value to call each var inside a stat list
//     const int BASE_INTENSITY = 0;
//     const int SELF_INTENSITY = 1;
//     const int SELF_DURATION = 2;
//     const int APPLY_INTENSITY = 3;
//     const int APPLY_DURATION = 4;
//     const int CAP_INTENSITY = 5;
//     const int CAP_DURATION = 6;
//     const int STARTING_INTENSITY = 7;
//     const int PASSED_TIME = 8;

//     //every stat in game
//     public enum Stats
//     {
//         Health,
//         Burning,
//         Dirty
//     }

//     //Stats list of the game object

//     [Tooltip ("")]
//     [SerializeField] public float[] health;
//     [SerializeField] public float[] burning;
//     [SerializeField] public float[] chilling;
//     [SerializeField] public float[] dirty;
    


//     //Create dict var
//     public Dictionary<Stats, float[]> statsDict;

//     #endregion
//     //========================


//     //FUNCTIONS
//     //========================
//     #region

//     /// <summary>
//     /// Applies the a stat on the object calling this function
//     /// </summary>
//     /// <param name="stat">The stat being applied</param>
//     /// <param name="intensity">How much intensity is being added</param>
//     /// <param name="duration">How much duration is being added</param>
//     public void ApplyStatSelf(Stats stat, float intensity, float duration)
//     {
//         statsDict[stat][SELF_INTENSITY] += intensity;
//         statsDict[stat][SELF_DURATION] += duration;
//         statsDict[stat][STARTING_INTENSITY] = statsDict[stat][SELF_INTENSITY];
//     }

//     public void AddToSelfApply(Stats stat, float intensity, float duration)
//     {
//         statsDict[stat][APPLY_INTENSITY] += intensity;
//         statsDict[stat][APPLY_DURATION] += duration;
//     } 

//     void DriftTowardsBase()
//     {
//         foreach (KeyValuePair<Stats, float[]> pair in statsDict)
//         {
//             float selfIntensity = pair.Value[SELF_INTENSITY];
//             float baseIntensity = pair.Value[BASE_INTENSITY];
//             float startingIntensity = pair.Value[STARTING_INTENSITY];
            
//             //move self intensity towards base, based on how much of the duration passed
//             if (selfIntensity != baseIntensity)
//             {
//                 float selfDuration = pair.Value[SELF_DURATION];
//                 float passedTime = pair.Value[PASSED_TIME];

//                 //get the percentage of durtation passed based on time passed
//                 float durationPercentage = Mathf.Clamp01(passedTime / selfDuration);
                
//                 //set self intensity to percentage between starting and base value (percentage of time passed from toal duration) 
//                 selfIntensity = Mathf.Lerp(startingIntensity, baseIntensity, durationPercentage);

//                 //Update values on dict
//                 pair.Value[SELF_INTENSITY] = selfIntensity;
//                 pair.Value[PASSED_TIME] += Time.deltaTime;

//                 print(pair.Value[PASSED_TIME]);
//                 print(selfIntensity);
//             }

//             //passed time to 0 if base and self intensity are the same
//             else if (pair.Value[PASSED_TIME] != 0 | pair.Value[SELF_DURATION] != 0)
//             {
//                 pair.Value[PASSED_TIME] = 0;
//                 pair.Value[SELF_DURATION] = 0;
//             }
//         }
//     }

//     #endregion
//     //========================


//     //RUNNING
//     //========================
//     #region

//     void Start()
//     {
//         if(statsDict == null)
//         {
//             statsDict = new Dictionary<Stats, float[]>()
//         {
//             {Stats.Health, health},
//             {Stats.Burning, burning},
//             {Stats.Dirty, dirty}
//         };
//         }
//         // statsDict[Stats.Burning][APPLY_INTENSITY] = 67;
//     }

//     void Update()
//     {
//         // print(gameObject.name + " " + statsDict[Stats.Burning][3]);
//         DriftTowardsBase();
//     }

//     #endregion
//     //========================


// }

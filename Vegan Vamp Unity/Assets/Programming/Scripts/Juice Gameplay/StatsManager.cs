using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StatsManager : MonoBehaviour
{
    //EXPLANATION
    //========================
    #region

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

    #endregion
    //========================


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
    #region

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] health;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] fire;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] ice;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] tornado;
    
    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] speedy;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] noGravity;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] teleport;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] grow;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] shrink;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] dunno;
    
    #endregion

    //Create jagged array
    public float[][] statsArray;


    //object type
    public enum Type
    {
        None,
        Ingredient,
        NPC,
        Player,
        Other
    }

    [SerializeField] public Type objectType;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

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
        statsArray[statNum][STARTING_BASE] = statsArray[statNum][CURRENT_BASE];
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
        for (int i = 0; i < statsArray.Length; i++)
        {
            float[] stat = statsArray[i];

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
            else if (currentBase != defaultBase && selfIntensity == currentBase)
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

    void CapValues()
    {
       foreach (float[] stat in statsArray)
       {
           if (stat[CURRENT_BASE] > stat[CAP_INTENSITY] && stat[CAP_INTENSITY] >= 0)
           {
               stat[CURRENT_BASE] = stat[CAP_INTENSITY];
           }

           if (stat[SELF_REACH_TIME] > stat[CAP_REACH_TIME] && stat[CAP_REACH_TIME] >= 0)
           {
               stat[SELF_REACH_TIME] = stat[CAP_REACH_TIME];
           }

           if (stat[SELF_RETURN_TIME] > stat[CAP_RETURN_TIME] && stat[CAP_RETURN_TIME] >= 0)
           {
               stat[SELF_RETURN_TIME] = stat[CAP_RETURN_TIME];
           }

           if (stat[CURRENT_BASE] < 0)
           {
               stat[CURRENT_BASE] = 0;
           }

           if (stat[DEFAULT_BASE] < 0)
           {
               stat[DEFAULT_BASE] = 0;
           }
       }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Awake()
    {
        if (statsArray == null)
        {
            statsArray = new float[][] {health, fire, ice, tornado, speedy, noGravity, teleport, grow, shrink, dunno};
        }   
    }

    void Update()
    {
        CapValues();
        DriftTowardsBase();
    }

    #endregion
    //========================
}

//GLOBAL CONSTANTS CLASS
public static class StatsConst
{ 
    public const int DEFAULT_BASE = 0;
    public const int CURRENT_BASE = 1;
    public const int SELF_INTENSITY = 2;
    public const int SELF_REACH_TIME = 3;
    public const int SELF_RETURN_TIME = 4;
    public const int APPLY_INTENSITY = 5;
    public const int APPLY_REACH_TIME = 6;
    public const int APPLY_RETURN_TIME = 7;
    public const int CAP_INTENSITY = 8;
    public const int CAP_REACH_TIME = 9;
    public const int CAP_RETURN_TIME = 10;
    public const int STARTING_BASE = 11;
    public const int STARTING_INTENSITY = 12;
    public const int PASSED_TIME = 13;

    //Stats order
    public const int HEALTH = 0;
    public const int FIRE = 1;
    public const int ICE = 2; 
    public const int TORNADO = 3;
}


//CLASSES TO CREATE THE NAME ARRAY THINGY
//========================
#region 

public class NamedArrayAttribute : PropertyAttribute
{
    public string[] names;

    public NamedArrayAttribute(params string[] names)
    {
        this.names = names;
    }
}

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        NamedArrayAttribute namedArray = attribute as NamedArrayAttribute;

        if (property.propertyType == SerializedPropertyType.ArraySize)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        else if (property.propertyType == SerializedPropertyType.Float)
        {
            int index = GetIndex(property);
            
            if (index >= 0 && index < namedArray.names.Length)
            {
                label.text = namedArray.names[index];
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    private int GetIndex(SerializedProperty property)
    {
        string path = property.propertyPath;
        string indexStr = path.Substring(path.LastIndexOf("[") + 1);
        indexStr = indexStr.Substring(0, indexStr.Length - 1);

        if (int.TryParse(indexStr, out int index))
        {
            return index;
        }
        return -1;
    }
}

#endregion
//========================
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

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

    [HideInInspector] public bool dead = false;
    [HideInInspector] public float speedMultiplier;

    //Stats list of the game object
    #region

#if UNITY_EDITOR
    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] health;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] fire;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] ice;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] tornado;

    [NamedArrayAttribute("Default Base", "Current Base", "Self Intensity", "Self Reach Time", "Self Return Time", "Apply Intensity", "Apply Reach Time", "Apply Return Time", "Cap Intensity", "Cap Reach Time", "Cap Return Time", "Starting Base", "Starting Intensity", "Passed Time")]
    public float[] speed;

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

#else

    public float[] health;

    public float[] fire;

    public float[] ice;

    public float[] tornado;

    public float[] speed;

    public float[] noGravity;

    public float[] teleport;

    public float[] grow;

    public float[] shrink;

    public float[] dunno;

#endif

    #endregion

    [SerializeField] public List<Color> colors;

    public Dictionary<string, int> descriptionDict = new Dictionary<string, int>() {
        { "Health Ingredient", 0 },
        { "Fire Ingredient", 0 },
        { "Ice Ingredient", 0 },
        { "Tornado Ingredient", 0 },
        { "Speed Ingredient", 0 },
        { "Gravity Ingredient", 0 },
        { "Teleport Ingredient", 0 },
    };

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
        statsArray[statNum][StatsConst.CURRENT_BASE] += intensity;

        statsArray[statNum][StatsConst.SELF_REACH_TIME] += reachTime;
        statsArray[statNum][StatsConst.STARTING_INTENSITY] = statsArray[statNum][StatsConst.SELF_INTENSITY];

        statsArray[statNum][StatsConst.SELF_RETURN_TIME] += returnTime;
        statsArray[statNum][StatsConst.STARTING_BASE] = statsArray[statNum][StatsConst.CURRENT_BASE];
    }

    public void ApplyToBase(int statNum, float intensity, float returnTime = -1)
    {
        statsArray[statNum][StatsConst.DEFAULT_BASE] += intensity;
        statsArray[statNum][StatsConst.STARTING_BASE] = statsArray[statNum][StatsConst.DEFAULT_BASE];

        if (returnTime >= 0)
        {
            statsArray[statNum][StatsConst.SELF_RETURN_TIME] = returnTime;
        }

        else
        {
            statsArray[statNum][StatsConst.SELF_RETURN_TIME] = 0.001f;
        }
    }

    public void AddToSelfApply(int statNum, float intensity, float reachTime, float returnTime)
    {
        statsArray[statNum][StatsConst.APPLY_INTENSITY] += intensity;
        statsArray[statNum][StatsConst.APPLY_REACH_TIME] += reachTime;
        statsArray[statNum][StatsConst.APPLY_RETURN_TIME] += returnTime;
    } 

    public void CopyStats(StatsManager copyStats)
    {
        //stats
        for (int i = 0; i < statsArray.Length; i++)
        {
            for(int j = 0; j < statsArray[i].Length; j++)
            {
                statsArray[i][j] = copyStats.statsArray[i][j];
            }
        }

        //colors
        colors = new List<Color>();

        foreach (Color color in copyStats.colors)
        {
            colors.Add(color);
        }

        //description
        descriptionDict = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> pair in copyStats.descriptionDict)
        {
            descriptionDict.Add(pair.Key, pair.Value);
        }
    }

    public void PasteStats(StatsManager targetStats)
    {
        //stats
        for (int i = 0; i < statsArray.Length; i++)
        {
            for(int j = 0; j < statsArray[i].Length; j++)
            {
                targetStats.statsArray[i][j] = statsArray[i][j];
            }
        }

        //colors
        targetStats.colors = new List<Color>();

        foreach (Color color in colors)
        {
            targetStats.colors.Add(color);
        }

        //description
        targetStats.descriptionDict = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> pair in descriptionDict)
        {
            targetStats.descriptionDict.Add(pair.Key, pair.Value);
        }
    }

    public void ResetSelf()
    {
        foreach (float[] stat in statsArray)
        {
            for (int i = 0; i < 9; i++)
            {
                if (stat[i] != 0)
                {
                    stat[i] = 0;
                }
            }
        }

        //reset to default color
        colors = new List<Color>();

        //reset description
        foreach (string key in descriptionDict.Keys.ToList())
        {
            descriptionDict[key] = 0;
        }
    }

    void DriftTowardsBase()
    {
        for (int i = 0; i < statsArray.Length; i++)
        {
            float[] stat = statsArray[i];

            float defaultBase = stat[StatsConst.DEFAULT_BASE];
            float currentBase = stat[StatsConst.CURRENT_BASE];
            float selfIntensity = stat[StatsConst.SELF_INTENSITY];

            //move self intensity towards current base, based on how much of the reach time
            if (selfIntensity != currentBase)
            {
                float reachTime = stat[StatsConst.SELF_REACH_TIME];
                float passedTime = stat[StatsConst.PASSED_TIME];
                float startingIntensity = stat[StatsConst.STARTING_INTENSITY];

                //get the percentage of durtation passed based on time passed
                float reachTimePercentage = Mathf.Clamp01(passedTime / reachTime);
                
                //set self intensity to percentage between starting and base value (percentage of time passed from toal duration) 
                selfIntensity = Mathf.Lerp(startingIntensity, currentBase, reachTimePercentage);

                //Update values on dict
                stat[StatsConst.SELF_INTENSITY] = selfIntensity;
                stat[StatsConst.PASSED_TIME] += Time.deltaTime;
            }


            //move current base towards default base, based on how much of the return time
            else if (currentBase != defaultBase)
            {
                float returnTime = stat[StatsConst.SELF_RETURN_TIME];
                float reachTime = stat[StatsConst.SELF_REACH_TIME];
                float passedTime = stat[StatsConst.PASSED_TIME];
                float startingBase = stat[StatsConst.STARTING_BASE];

                //get the percentage of durtation passed based on time passed
                float returnTimePercentage = Mathf.Clamp01(passedTime / (returnTime + reachTime));

                //set self intensity to percentage between starting and base value (percentage of time passed from toal duration) 
                currentBase = Mathf.Lerp(startingBase, defaultBase, returnTimePercentage);

                //Update values on dict and keep intensity equal to current base (this might makeintensity unchangeble)
                stat[StatsConst.CURRENT_BASE] = currentBase;
                stat[StatsConst.SELF_INTENSITY] = currentBase;
                stat[StatsConst.PASSED_TIME] += Time.deltaTime;
            }

            //passed time to 0 if base and self intensity are the same
            else if (stat[StatsConst.PASSED_TIME] != 0 | stat[StatsConst.SELF_REACH_TIME] != 0 | stat[StatsConst.SELF_RETURN_TIME] != 0)
            {
                stat[StatsConst.PASSED_TIME] = 0;
                stat[StatsConst.SELF_REACH_TIME] = 0;
                stat[StatsConst.SELF_RETURN_TIME] = 0;
            }
        }
    }

    void CapValues()
    {
       foreach (float[] stat in statsArray)
       {
           if (stat[StatsConst.CURRENT_BASE] > stat[StatsConst.CAP_INTENSITY] && stat[StatsConst.CAP_INTENSITY] >= 0)
           {
               stat[StatsConst.CURRENT_BASE] = stat[StatsConst.CAP_INTENSITY];
           }

           if (stat[StatsConst.SELF_REACH_TIME] > stat[StatsConst.CAP_REACH_TIME] && stat[StatsConst.CAP_REACH_TIME] >= 0)
           {
               stat[StatsConst.SELF_REACH_TIME] = stat[StatsConst.CAP_REACH_TIME];
           }

           if (stat[StatsConst.SELF_RETURN_TIME] > stat[StatsConst.CAP_RETURN_TIME] && stat[StatsConst.CAP_RETURN_TIME] >= 0)
           {
               stat[StatsConst.SELF_RETURN_TIME] = stat[StatsConst.CAP_RETURN_TIME];
           }

           if (stat[StatsConst.CURRENT_BASE] < 0)
           {
               stat[StatsConst.CURRENT_BASE] = 0;
           }

           if (stat[StatsConst.DEFAULT_BASE] < 0)
           {
               stat[StatsConst.DEFAULT_BASE] = 0;
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
            statsArray = new float[][] {health, fire, ice, tornado, speed, noGravity, teleport, grow, shrink, dunno};
        }   
    }

    void Update()
    {
        CapValues();
        DriftTowardsBase();

        if (health[StatsConst.SELF_INTENSITY] <= 0)
        {
            dead = true;
        }
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
    public const int SPEED = 4;
    public const int GRAVITY = 5;
    public const int TELEPORT = 6;
    public const int GROW = 7;
    public const int SHRINK = 8;
}

////CLASSES TO CREATE THE NAME ARRAY THINGY
////========================
#region 

#if UNITY_EDITOR
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
#endif

#endregion
////========================

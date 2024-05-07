using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    //Status classes
    [Serializable]
    public class Stats
    {
        [Serializable]
        public class Dirty : Stats
        {
            public float intensity;
            public float duration;
        }
    }

    //Stats vars
    [SerializeField] public Stats.Dirty dirty = new Stats.Dirty();

    //Stats list
    Stats[] statsList = {new Stats.Dirty()};

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void ApplyStat(Stats statGiven, float intensity, float duration)
    {
        //get each stat type
        foreach (Stats statRead in statsList)
        {
            print(statRead.GetType());
            print(statGiven.GetType());
            //see if the stat type is the same as the one we wanna change
            if (statRead.GetType() == statGiven.GetType())
            {
                //get the subclass of the type and make a list of its fields (variables)
                Type subclass = statGiven.GetType();
                FieldInfo[] fields = subclass.GetFields();
                print(fields);

                //loop through each field and update it with the given values
                foreach (FieldInfo field in fields)
                {
                    if (field.Name == "intensity")
                    {
                        field.SetValue(statGiven, intensity);
                    }

                    if (field.Name == "duration")
                    {
                        field.SetValue(statGiven, duration);
                    }
                }
            }
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion
    //========================


}
